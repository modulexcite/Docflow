using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.DomainModels;
using RapidDoc.Filters;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Simple.ImageResizer;
using RapidDoc.Models.Grids;
using System.Runtime.Remoting;
using System.Reflection;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace RapidDoc.Controllers
{
    public class DocumentController : BasicController
    {
        private readonly IDocumentService _DocumentService;
        private readonly IProcessService _ProcessService;
        private readonly IWorkflowService _WorkflowService;
        private readonly IEmplService _EmplService;
        private readonly IAccountService _AccountService;
        private readonly ISystemService _SystemService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IReviewDocLogService _ReviewDocLogService;
        private readonly IDocumentReaderService _DocumentReaderService;
        private readonly ICommentService _CommentService;
        private readonly IEmailService _EmailService;
        private readonly IHistoryUserService _HistoryUserService;
        private readonly ISearchService _SearchService;

        public DocumentController(IDocumentService documentService, IProcessService processService, 
            IWorkflowService workflowService, IEmplService emplService, IAccountService accountService, ISystemService systemService,
            IWorkflowTrackerService workflowTrackerService, IReviewDocLogService reviewDocLogService,
            IDocumentReaderService documentReaderService, ICommentService commentService, IEmailService emailService,
            IHistoryUserService historyUserService, ISearchService searchService)
        {
            _DocumentService = documentService;
            _ProcessService = processService;
            _WorkflowService = workflowService;
            _EmplService = emplService;
            _AccountService = accountService;
            _SystemService = systemService;
            _WorkflowTrackerService = workflowTrackerService;
            _ReviewDocLogService = reviewDocLogService;
            _DocumentReaderService = documentReaderService;
            _CommentService = commentService;
            _EmailService = emailService;
            _HistoryUserService = historyUserService;
            _SearchService = searchService;
        }

        public ActionResult ArchiveDocuments()
        {
            return View();
        }

        public ActionResult GetAllDocument()
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), 1, false);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public ActionResult GetArchiveDocument()
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), 1, false);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public JsonResult GetDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArchiveDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), page, true);

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDocument(Guid id, bool isAfterView = false)
        {
            var previousModelState = TempData["ModelState"] as ModelStateDictionary;
            if (previousModelState != null)
            {
                foreach (KeyValuePair<string, ModelState> kvp in previousModelState)
                    if (!ModelState.ContainsKey(kvp.Key))
                        ModelState.Add(kvp.Key, kvp.Value);
            }

            DocumentTable docuTable = _DocumentService.Find(id);
            ProcessView process = _ProcessService.FindView(docuTable.ProcessTableId);
            if (docuTable == null || process == null || _DocumentService.isShowDocument(docuTable.Id, process.Id ?? Guid.Empty, "", isAfterView) == false)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            _ReviewDocLogService.SaveDomain(new ReviewDocLogTable { DocumentTableId = id });

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == docuTable.ApplicationUserCreatedId);
            ApplicationUser userTable = _AccountService.Find(docuTable.ApplicationUserCreatedId);
            if (emplTable == null || userTable == null)
            {
                return HttpNotFound();
            }

            object viewModel = InitialViewShowDocument(id, process, docuTable, userTable, emplTable);

            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult ShowDocument(Guid id, string approveDoc, string rejectDoc, IDictionary<string, object> documentData, string lastComment = "")
        {
            DocumentTable docuTable = _DocumentService.Find(id);
            if (docuTable == null) return HttpNotFound();

            if (lastComment != "")
            {
                _CommentService.SaveDomain(new CommentTable { Comment = lastComment, DocumentTableId = id });
            }

            if(rejectDoc != String.Empty)
            {
                ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (userTable == null) return HttpNotFound();

                DateTime checkRejectDate = DateTime.UtcNow.AddMinutes(-5);
                if (!_CommentService.Contains(x => x.ApplicationUserCreatedId == userTable.Id && x.DocumentTableId == id && x.CreatedDate >= checkRejectDate))
                {
                    EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == docuTable.ApplicationUserCreatedId);
                    ProcessView process = _ProcessService.FindView(docuTable.ProcessTableId);
                    object viewModel = InitialViewShowDocument(id, process, docuTable, userTable, emplTable);
                    ModelState.AddModelError(string.Empty, UIElementRes.UIElement.RejectReason);

                    return View("~/Views/Document/ShowDocument.cshtml", viewModel);
                }
            }

            ProcessTable processTable = docuTable.ProcessTable;
            if (_DocumentService.isSignDocument(id, processTable.Id))
            {
                if (approveDoc != String.Empty)
                {
                    _WorkflowService.AgreementWorkflowApprove(id, processTable.TableName, documentData);
                }
                else if (rejectDoc != String.Empty)
                {
                    _WorkflowService.AgreementWorkflowReject(id, processTable.TableName, documentData);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private object InitialViewShowDocument(Guid id, ProcessView process, DocumentTable docuTable, ApplicationUser userTable, EmplTable emplTable)
        {
            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;

            docuTable.isSign = _DocumentService.isSignDocument(docuTable.Id, docuTable.ProcessTableId);
            docuTable.isArchive = _ReviewDocLogService.isArchive(docuTable.Id, "", userTable);
            viewModel.DocumentTable = docuTable;
            viewModel.docData = _DocumentService.GetDocumentView(id);
            viewModel.fileId = docuTable.FileId;
            viewModel.WFTrackerItems = _WorkflowTrackerService.GetPartialView(x => x.DocumentTableId == id);

            ViewBag.CreatedDate = _SystemService.ConvertDateTimeToLocal(userTable, docuTable.CreatedDate);
            if (emplTable != null)
            {
                ViewBag.Initiator = emplTable.ApplicationUserId != null ? emplTable.FullName : docuTable.ApplicationUserCreatedId;
                ViewBag.TitleName = emplTable.TitleTableId != null ? emplTable.TitleTable.TitleName : String.Empty;
                ViewBag.DepartmentName = emplTable.DepartmentTableId != null ? emplTable.DepartmentTable.DepartmentName : String.Empty;
                ViewBag.CompanyName = emplTable.CompanyTableId != null ? emplTable.CompanyTable.AliasCompanyName : String.Empty;
            }
            else
            {
                ViewBag.Initiator = String.Empty;
                ViewBag.TitleName = String.Empty;
                ViewBag.DepartmentName = String.Empty;
                ViewBag.CompanyName = String.Empty;
            }

            return viewModel;
        }

        public ActionResult Create(Guid id)
        {
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (userTable == null) return HttpNotFound();

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id);
            if (emplTable == null) return HttpNotFound();

            ProcessView process = _ProcessService.FindView(id);
            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;
            viewModel.docData = _DocumentService.RouteCustomModelView(process.TableName);
            viewModel.fileId = Guid.NewGuid();

            return View(viewModel);
        }

        public ActionResult GetDocumentData(dynamic modelDoc, Guid idProcess, string viewType)
        {
            ProcessView process = _ProcessService.FindView(idProcess);
            return PartialView("~/Views/Custom/" + process.TableName + "_" + viewType + ".cshtml", modelDoc);
        }

        public ActionResult GetAllComment(Guid documentId, string lastComment = "")
        {
            if(lastComment != String.Empty)
            {
                SaveComment(documentId, lastComment);
            }

            var model = _CommentService.GetPartialView(x => x.DocumentTableId == documentId);
            return PartialView("~/Views/Shared/Comments.cshtml", model);
        }

        [HttpPost]
        public void SaveComment(Guid id, string lastComment)
        {
            if (lastComment != "")
            {
                _CommentService.SaveDomain(new CommentTable { Comment = lastComment, DocumentTableId = id });
                _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = id, HistoryType = Models.Repository.HistoryType.NewComment });
                _EmailService.SendInitiatorCommentEmail(id, lastComment);
            }
        }

        [HttpPost]
        public ActionResult Create(Guid processId, dynamic docModel, Guid fileId, String actionModelName, IDictionary<string, object> documentData)
        {
            ProcessView process = _ProcessService.FindView(processId);

            if (ModelState.IsValid)
            {
                try
                {
                    //Save Document
                    var documentId = _DocumentService.SaveDocument(docModel, process.TableName, processId, fileId);

                    /*
                    for (int i = 0; i < 100; i++ )
                    {
                        _DocumentService.SaveDocument(docModel, process.TableName, processId, fileId);
                    }
                    */
                    _ReviewDocLogService.SaveDomain(new ReviewDocLogTable { DocumentTableId = documentId });
                    _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.NewDocument });
                    SaveSearchData(docModel, actionModelName, documentId);
                    _WorkflowService.RunWorkflow(documentId, process.TableName, documentData);
                    _EmailService.SendInitiatorEmail(documentId);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;
            viewModel.docData = docModel;
            viewModel.fileId = fileId;

            return View("Create", viewModel);
        }

        public ActionResult DocumentToArchive(Guid id)
        {
            DocumentTable docuTable = _DocumentService.Find(id);

            if (docuTable != null)
            {
                if (docuTable.DocumentState == Models.Repository.DocumentState.Closed 
                    || docuTable.DocumentState == Models.Repository.DocumentState.Cancelled
                    || docuTable.DocumentState == Models.Repository.DocumentState.Completed
                    || docuTable.DocumentState == Models.Repository.DocumentState.Created)
                {
                    ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    if (userTable == null) return HttpNotFound();

                    ReviewDocLogTable reviewTable = _ReviewDocLogService.FirstOrDefault(x => x.DocumentTableId == id && x.ApplicationUserCreatedId == userTable.Id);
                    if (reviewTable != null)
                    {
                        reviewTable.isArchive = true;
                        _ReviewDocLogService.SaveDomain(reviewTable, userTable.UserName);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ValidationRes.ValidationResource.ErrorToArchiveDocumentState);
                    TempData["ModelState"] = ModelState;
                }
            }

            return RedirectToAction("ShowDocument", new { id = id });
        }

        public ActionResult DocumentFromArchive(Guid id)
        {
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (userTable == null) return HttpNotFound();

            IEnumerable<ReviewDocLogTable> reviewTables = _ReviewDocLogService.GetPartial(x => x.DocumentTableId == id && x.ApplicationUserCreatedId == userTable.Id && x.isArchive == true);

            if (reviewTables != null)
            {
                foreach (var reviewTable in reviewTables)
                {
                    reviewTable.isArchive = false;
                    _ReviewDocLogService.SaveDomain(reviewTable);
                }
            }

            return RedirectToAction("ShowDocument", new { id = id });
        }

        public ActionResult AddReader(Guid id)
        {
            ViewBag.DocumentId = id;
            var empls = _EmplService.GetPartialIntercompanyView(x => x.ApplicationUserId != null);

            foreach(var empl in empls)
            {
                if (_DocumentReaderService.Contains(x => x.DocumentTableId == id && x.UserId == empl.ApplicationUserId))
                {
                    empl.isActiveDualList = true;
                }
            }

            return View(empls);
        }

        [HttpPost]
        public ActionResult AddReader(Guid id, string[] listdata, bool? isAjax)
        {
            if (isAjax == true)
            {
                List<string> newReader = _DocumentReaderService.SaveReader(id, listdata);
                _EmailService.SendReaderEmail(id, newReader);
            }
            return RedirectToAction("ShowDocument", new { id = id });
        }

        public ActionResult AddExecutor(Guid id, string activityId)
        {
            ViewBag.DocumentId = id;
            var empls = _EmplService.GetPartialIntercompanyView(x => x.ApplicationUserId != null);

            WFTrackerTable tracker = _WorkflowTrackerService.FirstOrDefault(x => x.ActivityID == activityId && x.DocumentTableId == id);

            if (tracker.Users != null)
            {
                foreach (var empl in empls)
                {
                    foreach (var user in tracker.Users)
                    {
                        if(empl.ApplicationUserId == user.UserId)
                        {
                            empl.isActiveDualList = true;
                        }
                    }
                }
            }

            return View(empls);
        }

        [HttpPost]
        public ActionResult AddExecutor(Guid id, string activityId, string[] listdata, bool? isAjax)
        {
            if (isAjax == true)
            {
                WFTrackerTable tracker = _WorkflowTrackerService.FirstOrDefault(x => x.DocumentTableId == id && x.ActivityID == activityId);
                if(tracker != null)
                {
                    ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    if (userTable == null) return HttpNotFound();

                    foreach (string data in listdata)
                    {
                        if (tracker.Users.Exists(x => x.UserId == data) == false)
                        {
                            _EmailService.SendNewExecutorEmail(id, data);
                        }
                    }

                    tracker.Users.RemoveAll(x => x.InitiatorUserId != null);

                    if (listdata != null)
                    {
                        foreach (string data in listdata)
                        {
                            tracker.Users.Add(new WFTrackerUsersTable { UserId = data, InitiatorUserId = userTable.Id });
                        }
                    }

                    _WorkflowTrackerService.SaveDomain(tracker);
                }
            }
            return RedirectToAction("ShowDocument", new { id = id });
        }

        public JsonResult AjaxUpload(HttpPostedFileBase filelist, Guid documentFileId)
        {
            var statuses = new List<ViewDataUploadFilesResult>();
            System.IO.FileStream inFile;
            byte[] binaryData;
            string contentType;

            if (filelist != null && !string.IsNullOrEmpty(filelist.FileName) && documentFileId != Guid.Empty)
            {
                BinaryReader binaryReader = new BinaryReader(filelist.InputStream);
                byte[] data = binaryReader.ReadBytes(filelist.ContentLength);

                var thumbnail = new byte[] {  };
                contentType = filelist.ContentType.ToString().ToUpper();
                thumbnail = GetThumbnail(data, contentType);

                // here you can save your file to the database...
                FileTable doc = new FileTable();
                doc.DocumentFileId = documentFileId;
                doc.FileName = filelist.FileName;
                doc.ContentType = contentType;
                doc.ContentLength = filelist.ContentLength;
                doc.Data = data;
                doc.Thumbnail = thumbnail;

                Guid Id = _DocumentService.SaveFile(doc);

                if (thumbnail.Length == 0)
                {
                    inFile = new System.IO.FileStream(Server.MapPath("~/Content/FileUpload/content-types/64/Text.png"),
                                System.IO.FileMode.Open,
                                System.IO.FileAccess.Read);
                    binaryData = new Byte[inFile.Length];
                    long bytesRead = inFile.Read(binaryData, 0,
                                         (int)inFile.Length);
                    inFile.Close();
                    thumbnail = binaryData;
                }

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = doc.FileName,
                    size = doc.ContentLength,
                    url = @"/Document/DownloadFile/" + Id.ToString(),
                    deleteUrl = @"/Document/DeleteFile/" + Id.ToString(),
                    thumbnailUrl = @"data:image/png;base64," + Convert.ToBase64String(thumbnail),
                    deleteType = "DELETE"
                });
            }

            var uploadedFiles = new
            {
                files = statuses.ToArray()
            };

            JsonResult result = Json(uploadedFiles);
            result.ContentType = "text/plain";
            return result;
        }

        [HttpGet]
        public JsonResult GetAllFileDocument(Guid id)
        {
            var statuses = new List<ViewDataUploadFilesResult>();

            var files = _DocumentService.GetAllFilesDocument(id);

            foreach (var file in files)
            {
                var thumbnail = new byte[] { };
                System.IO.FileStream inFile;
                byte[] binaryData;

                if (file.Thumbnail.Length == 0)
                {
                    inFile = new System.IO.FileStream(Server.MapPath("~/Content/FileUpload/content-types/64/Text.png"),
                                System.IO.FileMode.Open,
                                System.IO.FileAccess.Read);
                    binaryData = new Byte[inFile.Length];
                    long bytesRead = inFile.Read(binaryData, 0,
                                         (int)inFile.Length);
                    inFile.Close();
                    thumbnail = binaryData;
                }
                else
                {
                    thumbnail = file.Thumbnail;
                }


                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    url = @"/Document/DownloadFile/" + file.Id.ToString(),
                    deleteUrl = @"/Document/DeleteFile/" + file.Id.ToString(),
                    thumbnailUrl = @"data:image/png;base64," + Convert.ToBase64String(thumbnail),
                    deleteType = "DELETE"
                });
            }

            var uploadedFiles = new
            {
                files = statuses.ToArray()
            };

            JsonResult result = Json(uploadedFiles, JsonRequestBehavior.AllowGet);
            result.ContentType = "text/plain";
            return result;
        }

        private byte[] GetThumbnail(byte[] fileData, string contentType)
        {
            var thumbnail = new byte[] {  };

            if (contentType == "IMAGE/PNG"
                || contentType == "IMAGE/GIF"
                || contentType == "IMAGE/JPEG"
                || contentType == "IMAGE/BMP")
            {
                thumbnail = ImageResizer(fileData);
            }

            return thumbnail;
        }

        private byte[] ImageResizer(byte[] entireImage)
        {
            try
            {
                ImageResizer resizer = new ImageResizer(entireImage);
                return resizer.Resize(64, 64, false, ImageEncoding.Png);
            }
            catch (Exception e)
            {
                return new byte[] { };
            }
        }

        [HttpGet]
        public FileContentResult DownloadFile(Guid Id)
        {
            FileTable fileTable = _DocumentService.GetFile(Id);
            return File(fileTable.Data, fileTable.ContentType, fileTable.FileName);
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public JsonResult DeleteFile(Guid Id)
        {
            Dictionary<string, bool> values = new Dictionary<string, bool>();
            string fileName = _DocumentService.DeleteFile(Id);
            values.Add(fileName, true);

            var deletedFiles = new
            {
                files = values
            };

            JsonResult result = Json(deletedFiles);
            result.ContentType = "text/plain";
            return result;
        }

        public class ViewDataUploadFilesResult
        {
            public string name { get; set; }
            public int size { get; set; }
            public string url { get; set; }
            public string deleteUrl { get; set; }
            public string thumbnailUrl { get; set; }
            public string deleteType { get; set; }
        }

        public ActionResult RoutePostMethod(Guid processId, dynamic docModel, int type, Guid? documentId, Guid fileId, String actionModelName, HttpPostedFileBase file, IDictionary<string, object> documentData, string approveDoc = "", string rejectDoc = "", string lastComment = "")
        {
            ActionResult view;
            Guid localDocumentId = documentId ?? Guid.Empty;

            if (file != null)
            {
                return view = AjaxUpload(file, fileId);
            }

            switch(type)
            {
                case 1:
                    view = Create(processId, docModel, fileId, actionModelName, documentData);
                    break;
                case 2:
                    view = ShowDocument(localDocumentId, approveDoc, rejectDoc, documentData, lastComment);
                    break;
                default:
                    view = View();
                    break;
            }

            return view;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostDocument(Guid processId, int type, Guid? documentId, Guid fileId, HttpPostedFileBase files, FormCollection collection, string actionModelName, string approveDoc = "", string rejectDoc = "", string lastComment = "")
        {
            IDictionary<string, object> documentData = new Dictionary<string, object>();
            Type typeActionModel = Type.GetType("RapidDoc.Models.ViewModels." + actionModelName + "_View");
            var actionModel = Activator.CreateInstance(typeActionModel);

            foreach (var key in collection.AllKeys)
            {
                if (actionModel.GetType().GetProperty(key) != null)
                {
                    System.Reflection.PropertyInfo propertyInfo = actionModel.GetType().GetProperty(key);

                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        var valueEnum = Enum.Parse(propertyInfo.PropertyType, collection[key].ToString(), true);
                        propertyInfo.SetValue(actionModel, valueEnum, null);
                        documentData.Add(key, valueEnum);
                    }
                    else if(propertyInfo.PropertyType == typeof(bool))
                    {
                        bool valueBool = collection[key].Contains("true");
                        propertyInfo.SetValue(actionModel, valueBool, null);
                        documentData.Add(key, valueBool);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime?))
                    {
                        DateTime? valueDate = collection[key] == "" ? null : (DateTime?)DateTime.Parse(collection[key]);
                        propertyInfo.SetValue(actionModel, valueDate, null);
                        documentData.Add(key, valueDate);
                    }
                    else
                    {
                        bool isRequired = actionModel.GetType().GetProperty(key)
                                .GetCustomAttributes(typeof(RequiredAttribute), false)
                                .Length == 1;

                        if ((isRequired == true && !String.IsNullOrEmpty(collection[key]) && !String.IsNullOrWhiteSpace(collection[key]) && collection[key] != "<p><br></p>") || (isRequired == false))
                        {
                            var value = Convert.ChangeType(collection[key], propertyInfo.PropertyType);
                            propertyInfo.SetValue(actionModel, value, null);
                            documentData.Add(key, value);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorFieldisNull, key));
                        }
                    }
                }
            }

            CheckCustomDocument(typeActionModel, actionModel);

            ActionResult view = RoutePostMethod(processId, actionModel, type, documentId, fileId, actionModelName, files, documentData, approveDoc, rejectDoc, lastComment);
            return view;
        }

        private void CheckCustomDocument(Type type, dynamic actionModel)
        {
            if(type == (new USR_REQ_IT_CTS_SetPersonalButton_View()).GetType())
            {
                if (actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo01 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo01) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo01)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 1. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo01));
                }
                if (actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo02 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo02) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo02)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 2. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo02));
                }
                if (actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo03 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo03) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo03)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 3. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo03));
                }
                if (actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo04 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo04) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo04)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 4. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo04));
                }
                if (actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo05 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo05) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo05)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 5. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo05));
                }
                if (actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo06 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo06) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo06)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 6. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo06));
                }
                if (actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo07 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo07) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo07)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 7. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo07));
                }
                if (actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo08 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo08) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo08)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 8. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo08));
                }
                if (actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo09 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo09) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo09)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 9. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo09));
                }
                if (actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo10 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo10) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo10)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 10. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo10));
                }
                if (actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo11 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo11) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo11)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 11. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo11));
                }
                if (actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo12 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo12) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo12)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 12. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo12));
                }
                if (actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo13 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo13) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo13)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 13. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo13));
                }
                if (actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo14 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo14) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo14)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 14. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo14));
                }
                if (actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo15 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo15) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo15)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 15. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo15));
                }
                if (actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.None && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.CallAppr && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.AbrProg && actionModel.ServiceTypeButtonNo16 != RapidDoc.Models.Repository.ServiceType.DnDst && (String.IsNullOrEmpty(actionModel.TextButtonNo16) || String.IsNullOrWhiteSpace(actionModel.TextButtonNo16)))
                {
                    ModelState.AddModelError(string.Empty, String.Format("Кнопка 16. Если указан сервис {0}, то нужно заполнить соответствующее поле номер телефона", actionModel.ServiceTypeButtonNo16));
                }
            }

            if (type == (new USR_REQ_IT_CAP_RemoveSignLotus_View()).GetType())
            {
                if (actionModel.DocumentDate == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата должна быть заполнена");
                }
            }

            if (type == (new USR_REQ_IT_CAP_DelegationExchServ_View()).GetType())
            {
                if (actionModel.FromDate == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата с должна быть заполнена");
                }
                if (actionModel.ToDate == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата по должна быть заполнена");
                }
            }

            if (type == (new USR_REQ_IT_CAP_DelegationDocflow_View()).GetType())
            {
                if (actionModel.FromDate == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата с должна быть заполнена");
                }
                if (actionModel.ToDate == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата по должна быть заполнена");
                }
            }

            if (type == (new USR_REQ_IT_CAP_CreateUserAD_View()).GetType())
            {
                if (actionModel.BirthDay == null)
                {
                    ModelState.AddModelError(string.Empty, "Дата с должна быть заполнена");
                }
            }
        }

        private void SaveSearchData(dynamic docModel, string actionModelName, Guid documentId)
        {
            Type type = Type.GetType("RapidDoc.Models.ViewModels." + actionModelName + "_View");
            var properties = type.GetProperties();
            string allStringData = String.Empty;
            string regex = @"(<.+?>|&nbsp;)";
            string regexGuid = @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})";

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(docModel, null);

                    if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    {
                        string stringWithoutTags = Regex.Replace(value, regex, "").Trim();

                        if (!String.IsNullOrEmpty(stringWithoutTags))
                        {
                            List<string> guidList = new List<string>();
                            guidList = Regex.Matches(stringWithoutTags, regexGuid)
                                .Cast<Match>()
                                .Select(m => m.Groups[0].Value)
                                .ToList();

                            foreach (string guid in guidList)
                            {
                                stringWithoutTags = stringWithoutTags.Replace(guid + ",", "");
                                stringWithoutTags = stringWithoutTags.Replace(guid, "");
                            }

                            allStringData = allStringData + stringWithoutTags + "|";
                        }
                    }
                }
            }

            _SearchService.SaveDomain(new SearchTable { DocumentText = allStringData, DocumentTableId = documentId });
        }
	}
}