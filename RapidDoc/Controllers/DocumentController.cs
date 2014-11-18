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
using System.Data.Entity.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Controllers
{
    public class DocumentController : BasicController
    {
        private readonly IDocumentService _DocumentService;
        private readonly IProcessService _ProcessService;
        private readonly IWorkflowService _WorkflowService;
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IReviewDocLogService _ReviewDocLogService;
        private readonly IDocumentReaderService _DocumentReaderService;
        private readonly ICommentService _CommentService;
        private readonly IEmailService _EmailService;
        private readonly IHistoryUserService _HistoryUserService;
        private readonly ISearchService _SearchService;
        private readonly ICustomCheckDocument _CustomCheckDocument;

        public DocumentController(IDocumentService documentService, IProcessService processService, 
            IWorkflowService workflowService, IEmplService emplService, IAccountService accountService, ISystemService systemService,
            IWorkflowTrackerService workflowTrackerService, IReviewDocLogService reviewDocLogService,
            IDocumentReaderService documentReaderService, ICommentService commentService, IEmailService emailService,
            IHistoryUserService historyUserService, ISearchService searchService, ICompanyService companyService, ICustomCheckDocument customCheckDocument)
            : base(companyService, accountService)
        {
            _DocumentService = documentService;
            _ProcessService = processService;
            _WorkflowService = workflowService;
            _EmplService = emplService;
            _SystemService = systemService;
            _WorkflowTrackerService = workflowTrackerService;
            _ReviewDocLogService = reviewDocLogService;
            _DocumentReaderService = documentReaderService;
            _CommentService = commentService;
            _EmailService = emailService;
            _HistoryUserService = historyUserService;
            _SearchService = searchService;
            _CustomCheckDocument = customCheckDocument;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArchiveDocuments()
        {
            return View();
        }

        public ActionResult AgreedDocuments()
        {
            return View();
        }

        public ActionResult GetAllDocument()
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public ActionResult GetArchiveDocument()
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public JsonResult GetDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArchiveDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAgreedDocument()
        {
            var grid = new AgreedDocumentAjaxPagingGrid(_DocumentService.GetAgreedDocument(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public JsonResult GetAgreedDocumentList(int page)
        {
            var grid = new AgreedDocumentAjaxPagingGrid(_DocumentService.GetAgreedDocument(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

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

            DocumentView docuView = _DocumentService.FindView(id);
            ProcessView process = _ProcessService.FindView(GuidNull2Guid(docuView.ProcessTableId));
            if (docuView == null || process == null || _DocumentService.isShowDocument(GuidNull2Guid(docuView.Id), GuidNull2Guid(process.Id), "", isAfterView) == false)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            _ReviewDocLogService.SaveDomain(new ReviewDocLogTable { DocumentTableId = id });

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == docuView.ApplicationUserCreatedId && x.CompanyTableId == docuView.CompanyTableId);
            ApplicationUser userTable = _AccountService.Find(docuView.ApplicationUserCreatedId);
            if (emplTable == null || userTable == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            object viewModel = InitialViewShowDocument(id, process, docuView, userTable, emplTable);
            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult ShowDocument(Guid id, string approveDoc, string rejectDoc, IDictionary<string, object> documentData, string lastComment = "")
        {
            DocumentTable docuTable = _DocumentService.Find(id);
            if (docuTable == null) return RedirectToAction("PageNotFound", "Error");

            if (lastComment != "")
            {
                _CommentService.SaveDomain(new CommentTable { Comment = lastComment, DocumentTableId = id });
            }

            DocumentView docuView = _DocumentService.FindView(id);
            if(rejectDoc != String.Empty)
            {
                ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
                if (userTable == null) return RedirectToAction("PageNotFound", "Error");

                DateTime checkRejectDate = DateTime.UtcNow.AddMinutes(-5);
                HistoryUserTable history = _HistoryUserService.FirstOrDefault(x => x.DocumentTableId == id && x.HistoryType == Models.Repository.HistoryType.CancelledDocument);
                if (history != null && history.CreatedDate > checkRejectDate)
                {
                    checkRejectDate = history.CreatedDate;
                }

                if (!_CommentService.Contains(x => x.ApplicationUserCreatedId == userTable.Id && x.DocumentTableId == id && x.CreatedDate >= checkRejectDate))
                {
                    EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == docuTable.ApplicationUserCreatedId);
                    ProcessView process = _ProcessService.FindView(docuTable.ProcessTableId);
                    object viewModel = InitialViewShowDocument(id, process, docuView, userTable, emplTable);
                    ModelState.AddModelError(string.Empty, UIElementRes.UIElement.RejectReason);

                    return View("~/Views/Document/ShowDocument.cshtml", viewModel);
                }
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction("Index", "Document");
            }

            ApplicationUser userResult = _AccountService.Find(User.Identity.GetUserId());
            EmplTable emplResult = _EmplService.FirstOrDefault(x => x.ApplicationUserId == docuView.ApplicationUserCreatedId && x.CompanyTableId == userResult.CompanyTableId);
            ProcessView processResult = _ProcessService.FindView(GuidNull2Guid(docuView.ProcessTableId));
            object viewModelResult = InitialViewShowDocument(id, processResult, docuView, userResult, emplResult);

            return View("~/Views/Document/ShowDocument.cshtml", viewModelResult);
        }

        private object InitialViewShowDocument(Guid id, ProcessView process, DocumentView docuView, ApplicationUser userTable, EmplTable emplTable)
        {
            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;

            docuView.isSign = _DocumentService.isSignDocument(GuidNull2Guid(docuView.Id), GuidNull2Guid(docuView.ProcessTableId));
            docuView.isArchive = _ReviewDocLogService.isArchive(GuidNull2Guid(docuView.Id), "", userTable);
            viewModel.DocumentView = docuView;
            viewModel.docData = _DocumentService.GetDocumentView(id);
            viewModel.fileId = docuView.FileId;
            viewModel.WFTrackerItems = _WorkflowTrackerService.GetPartialView(x => x.DocumentTableId == id);

            ViewBag.CreatedDate = _SystemService.ConvertDateTimeToLocal(userTable, docuView.CreatedDate);
            if (emplTable != null)
            {
                ViewBag.Initiator = emplTable.ApplicationUserId != null ? emplTable.FullName : docuView.ApplicationUserCreatedId;
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

            ViewBag.RejectHistory = _HistoryUserService.GetPartialView(x => x.DocumentTableId == docuView.Id && x.HistoryType == Models.Repository.HistoryType.CancelledDocument);
            ViewBag.AddReaders = _HistoryUserService.GetPartialView(x => x.DocumentTableId == docuView.Id && x.HistoryType == Models.Repository.HistoryType.AddReader);
            ViewBag.RemoveReaders = _HistoryUserService.GetPartialView(x => x.DocumentTableId == docuView.Id && x.HistoryType == Models.Repository.HistoryType.RemoveReader);

            return viewModel;
        }

        public ActionResult Create(Guid id)
        {
            ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
            if (userTable == null) return RedirectToAction("PageNotFound", "Error");

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id && x.CompanyTableId == userTable.CompanyTableId);
            if (emplTable == null) return RedirectToAction("PageNotFound", "Error");

            ProcessView process = _ProcessService.FindView(id);

            DateTime date = DateTime.UtcNow;
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day) + process.StartWorkTime;
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day) + process.EndWorkTime;
            if ((startTime < date || date > endTime) && process.StartWorkTime != process.EndWorkTime) return RedirectToAction("PageNotFound", "Error");

            ApplicationDbContext context = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!String.IsNullOrEmpty(process.RoleId))
            {
                string roleName = RoleManager.FindById(process.RoleId).Name;
                if (!UserManager.IsInRole(userTable.Id, roleName))
                {
                    return RedirectToAction("PageNotFound", "Error");
                }
            }
            context.Dispose();

            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;
            viewModel.docData = _DocumentService.RouteCustomModelView(process.TableName);
            viewModel.fileId = Guid.NewGuid();
            viewModel.ProcessTemplates = _DocumentService.GetAllTemplatesDocument(process.Id.Value);

            return View(viewModel);
        }

        public ActionResult GetDocumentData(dynamic modelDoc, string tableName, string viewType)
        {
            return PartialView("~/Views/Custom/" + tableName + "_" + viewType + ".cshtml", modelDoc);
        }

        public ActionResult GetAllComment(Guid documentId, string lastComment = "")
        {
            if(lastComment != String.Empty)
            {
                SaveComment(documentId, lastComment);
            }

            var model = _CommentService.GetPartialView(x => x.DocumentTableId == documentId);
            return PartialView("~/Views/Shared/_Comments.cshtml", model);
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
                //Save Document
                var documentId = _DocumentService.SaveDocument(docModel, process.TableName, processId, fileId);
                _ReviewDocLogService.SaveDomain(new ReviewDocLogTable { DocumentTableId = documentId });
                _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.NewDocument });
                SaveSearchData(docModel, actionModelName, documentId);
                _WorkflowService.RunWorkflow(documentId, process.TableName, documentData);

                return RedirectToAction("Index", "Document");
            }

            var viewModel = new DocumentComposite();
            viewModel.ProcessView = process;
            viewModel.docData = docModel;
            viewModel.fileId = fileId;
            viewModel.ProcessTemplates = _DocumentService.GetAllTemplatesDocument(processId);

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
                    ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
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
            ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
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
            var empls = InitializeReaderView(id);
            return View(empls);
        }

        [HttpPost]
        public ActionResult AddReader(Guid id, string[] listdata, bool? isAjax)
        {
            if (isAjax == true)
            {
                try
                {
                    List<string> newReader = _DocumentReaderService.SaveReader(id, listdata);
                    _EmailService.SendReaderEmail(id, newReader);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    ViewBag.DocumentId = id;
                    var empls = InitializeReaderView(id);
                    return View(empls);
                }
            }

            return Json(new { result = "Redirect", url = Url.Action("ShowDocument", new { id = id, isAfterView = true }) });
        }

        private IEnumerable<EmplView> InitializeReaderView(Guid id)
        {
            var empls = _EmplService.GetPartialIntercompanyView(x => x.ApplicationUserId != null);

            foreach (var empl in empls)
            {
                if (_DocumentReaderService.Contains(x => x.DocumentTableId == id && x.UserId == empl.ApplicationUserId))
                {
                    empl.isActiveDualList = true;
                }
            }

            return empls;
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
                    ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
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
                            if (tracker.Users.Exists(x => x.UserId == data) == false)
                            {
                                tracker.Users.Add(new WFTrackerUsersTable { UserId = data, InitiatorUserId = userTable.Id });
                            }
                        }
                    }

                    _WorkflowTrackerService.SaveDomain(tracker);
                }
            }

            return Json(new { result = "Redirect", url = Url.Action("ShowDocument", new { id = id, isAfterView = true }) });
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
            catch (Exception)
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
                    _DocumentService.UpdateDocumentFields(docModel, processId);
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
                        bool valueBool = collection[key].ToLower().Contains("true");
                        propertyInfo.SetValue(actionModel, valueBool, null);
                        documentData.Add(key, valueBool);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime?))
                    {
                        DateTime? valueDate = collection[key] == "" ? null : (DateTime?)DateTime.Parse(collection[key]);
                        propertyInfo.SetValue(actionModel, valueDate, null);
                        documentData.Add(key, valueDate);
                    }
                    else if (propertyInfo.PropertyType == typeof(Guid?))
                    {
                        Guid? valueNotGuid = collection[key] == "" ? null : (Guid?)Guid.Parse(collection[key]);
                        propertyInfo.SetValue(actionModel, valueNotGuid, null);
                        documentData.Add(key, valueNotGuid);
                    }
                    else if (propertyInfo.PropertyType == typeof(Guid))
                    {
                        Guid valueGuid = Guid.Parse(collection[key]);
                        propertyInfo.SetValue(actionModel, valueGuid, null);
                        documentData.Add(key, valueGuid);
                    }
                    else if (propertyInfo.PropertyType == typeof(TimeSpan))
                    {
                        TimeSpan valueTimeSpan = TimeSpan.Parse(collection[key]);
                        propertyInfo.SetValue(actionModel, valueTimeSpan, null);
                        documentData.Add(key, valueTimeSpan);
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
                            ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorFieldisNull, GetAttributeDisplayName(propertyInfo)));
                        }
                    }
                }
            }

            if (files == null)
            {
                CheckCustomDocument(typeActionModel, actionModel);
                CheckAttachedFiles(processId, fileId, documentId);
                _CustomCheckDocument.PreUpdateViewModel(typeActionModel, actionModel);
            }

            ActionResult view = RoutePostMethod(processId, actionModel, type, documentId, fileId, actionModelName, files, documentData, approveDoc, rejectDoc, lastComment);
            return view;
        }

        private void CheckAttachedFiles(Guid processId, Guid fileId, Guid? documentId)
        {
            ProcessTable process = _ProcessService.Find(processId);
            var files = _DocumentService.GetAllFilesDocument(fileId).ToList();
            if(process.MandatoryNumberFiles > 0)
            {
                if(files.Count < process.MandatoryNumberFiles)
                {
                    ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorMandatoryNumberFiles, process.MandatoryNumberFiles, files.Count));
                }

                if (process.MandatoryFileTypes != null && process.MandatoryFileTypes != String.Empty)
                {
                    string[] fileTypes = process.MandatoryFileTypes.Split('|');

                    foreach(var file in files)
                    {
                        if(!fileTypes.Contains(Path.GetExtension(file.FileName.ToUpper())))
                        {
                            ModelState.AddModelError(string.Empty, String.Format(ValidationRes.ValidationResource.ErrorMandatoryFileTypes, process.MandatoryFileTypes, file.FileName));
                        }
                    }
                }
            }
        }

        private void CheckCustomDocument(Type type, dynamic actionModel)
        {
            List<string> errorList =  _CustomCheckDocument.CheckCustomDocument(type, actionModel);

            foreach(var error in errorList)
            {
                ModelState.AddModelError(string.Empty, error);
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

            DocumentTable document = _DocumentService.Find(documentId);
            document.DocumentText = allStringData;
            _DocumentService.SaveDocumentText(document);

            _SearchService.SaveDomain(new SearchTable { DocumentText = allStringData, DocumentTableId = documentId });
        }
	}
}