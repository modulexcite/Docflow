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
        private readonly IServiceIncidentService _ServiceIncidentService;
        private readonly ITripSettingsService _TripSettingsService;

        public DocumentController(IDocumentService documentService, IProcessService processService, 
            IWorkflowService workflowService, IEmplService emplService, IAccountService accountService, ISystemService systemService,
            IWorkflowTrackerService workflowTrackerService, IReviewDocLogService reviewDocLogService,
            IDocumentReaderService documentReaderService, ICommentService commentService, IEmailService emailService,
            IHistoryUserService historyUserService, ISearchService searchService, IServiceIncidentService serviceIncidentService, ICompanyService companyService, ITripSettingsService tripSettingsService)
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
            _ServiceIncidentService = serviceIncidentService;
            _TripSettingsService = tripSettingsService;
        }

        public ActionResult ArchiveDocuments()
        {
            return View();
        }

        public ActionResult GetAllDocument()
        {
            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public ActionResult GetArchiveDocument()
        {
            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public JsonResult GetDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetAllView(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArchiveDocumentList(int page)
        {
            var grid = new DocumentAjaxPagingGrid(_DocumentService.GetArchiveView(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAgreedDocument()
        {
            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            var grid = new AgreedDocumentAjaxPagingGrid(_DocumentService.GetAgreedDocument(), 1, false, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);
            return PartialView("~/Views/Document/DocumentList.cshtml", grid);
        }

        public JsonResult GetAgreedDocumentList(int page)
        {
            var grid = new AgreedDocumentAjaxPagingGrid(_DocumentService.GetAgreedDocument(), page, true, _ReviewDocLogService, _DocumentService, _AccountService, _SearchService, _EmplService);

            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            ViewBag.CurrentTimeId = user.TimeZoneId;

            return Json(new
            {
                Html = RenderPartialViewToString("DocumentList", grid),
                HasItems = grid.DisplayingItemsCount >= grid.Pager.PageSize
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgreedDocuments()
        {
            return View();
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
                return HttpNotFound();
            }

            object viewModel = InitialViewShowDocument(id, process, docuView, userTable, emplTable);
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

            DocumentView docuView = _DocumentService.FindView(id);
            if(rejectDoc != String.Empty)
            {
                ApplicationUser userTable = _AccountService.Find(User.Identity.GetUserId());
                if (userTable == null) return HttpNotFound();

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
                return RedirectToAction("Index", "Home");
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
            if (userTable == null) return HttpNotFound();

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id && x.CompanyTableId == userTable.CompanyTableId);
            if (emplTable == null) return HttpNotFound();

            ProcessView process = _ProcessService.FindView(id);

            ApplicationDbContext context = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!String.IsNullOrEmpty(process.RoleId))
            {
                string roleName = RoleManager.FindById(process.RoleId).Name;
                if (!UserManager.IsInRole(userTable.Id, roleName))
                {
                    return HttpNotFound();
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
                //Save Document
                var documentId = _DocumentService.SaveDocument(docModel, process.TableName, processId, fileId);
                _ReviewDocLogService.SaveDomain(new ReviewDocLogTable { DocumentTableId = documentId });
                _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.NewDocument });
                SaveSearchData(docModel, actionModelName, documentId);
                _WorkflowService.RunWorkflow(documentId, process.TableName, documentData);

                return RedirectToAction("Index", "Home");
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
                CheckCustomDocument(typeActionModel, actionModel, fileId);
                CheckAttachedFiles(processId, fileId, documentId);
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

        private void CheckCustomDocument(Type type, dynamic actionModel, Guid fileId)
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

            if (type == (new USR_REQ_IT_CTP_IncidentIT_View()).GetType())
            {
                if (actionModel.ServiceName == null && actionModel.Id != null)
                {
                    ModelState.AddModelError(string.Empty, "Не указан сервис ИТ");
                }

                if (actionModel.ServiceName != null && actionModel.Id != null)
                {
                    ServiceIncidentTable incidentTable = _ServiceIncidentService.GetAll().FirstOrDefault(x => x.ServiceName == actionModel.ServiceName && x.ServiceIncidientLevel == ((ServiceIncidientLevel)actionModel.ServiceIncidientLevel) && x.ServiceIncidientPriority == ((ServiceIncidientPriority)actionModel.ServiceIncidientPriority) && x.ServiceIncidientLocation == ((ServiceIncidientLocation)actionModel.ServiceIncidientLocation));

                    if(incidentTable == null)
                    {
                        ModelState.AddModelError(string.Empty, "Не правильно указан сервис ИТ");
                    }
                }
            }

            if (type == (new USR_REQ_OKS_RequestForVisa_View()).GetType())
            {
                if (actionModel.FromDate > actionModel.ToDate)
                {
                    ModelState.AddModelError(string.Empty, "Неверно указан диапазон дат");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportORZZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCNoneZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportTMCUZL_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportAsset_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportZIFOre_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportOSPVHZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportItems_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForHU_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForMovementItems_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForMovementAssets_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForImportORZZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UB_RequestForExportItemFromZIF_View()).GetType())
            {
                if (actionModel.Post1 == false && actionModel.Post2 == false && actionModel.Post3 == false && actionModel.Post4 == false && actionModel.Post5 == false && actionModel.Post6 == false)
                {
                    ModelState.AddModelError(string.Empty, "Не указан пост охраны");
                }
            }

            if (type == (new USR_REQ_UBP_RequestForExportWastes_View()).GetType())
            {
                if (actionModel.Number1 == true && (actionModel.Number32 == String.Empty || actionModel.Number63 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 1");
                }

                if (actionModel.Number2 == true && (actionModel.Number33 == String.Empty || actionModel.Number64 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 2");
                }

                if (actionModel.Number3 == true && (actionModel.Number34 == String.Empty || actionModel.Number65 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 3");
                }

                if (actionModel.Number4 == true && (actionModel.Number35 == String.Empty || actionModel.Number66 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 4");
                }

                if (actionModel.Number5 == true && (actionModel.Number36 == String.Empty || actionModel.Number67 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 5");
                }

                if (actionModel.Number6 == true && (actionModel.Number37 == String.Empty || actionModel.Number68 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 6");
                }

                if (actionModel.Number7 == true && (actionModel.Number38 == String.Empty || actionModel.Number69 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 7");
                }

                if (actionModel.Number8 == true && (actionModel.Number39 == String.Empty || actionModel.Number70 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 8");
                }

                if (actionModel.Number9 == true && (actionModel.Number40 == String.Empty || actionModel.Number71 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 9");
                }

                if (actionModel.Number10 == true && (actionModel.Number41 == String.Empty || actionModel.Number72 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 10");
                }

                if (actionModel.Number11 == true && (actionModel.Number42 == String.Empty || actionModel.Number73 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 11");
                }

                if (actionModel.Number12 == true && (actionModel.Number43 == String.Empty || actionModel.Number74 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 12");
                }

                if (actionModel.Number13 == true && (actionModel.Number44 == String.Empty || actionModel.Number75 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 13");
                }

                if (actionModel.Number14 == true && (actionModel.Number45 == String.Empty || actionModel.Number76 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 14");
                }

                if (actionModel.Number15 == true && (actionModel.Number46 == String.Empty || actionModel.Number77 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 15");
                }

                if (actionModel.Number16 == true && (actionModel.Number47 == String.Empty || actionModel.Number78 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 16");
                }

                if (actionModel.Number17 == true && (actionModel.Number48 == String.Empty || actionModel.Number79 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 17");
                }

                if (actionModel.Number18 == true && (actionModel.Number49 == String.Empty || actionModel.Number80 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 18");
                }

                if (actionModel.Number19 == true && (actionModel.Number50 == String.Empty || actionModel.Number81 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 19");
                }

                if (actionModel.Number20 == true && (actionModel.Number51 == String.Empty || actionModel.Number82 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 20");
                }

                if (actionModel.Number21 == true && (actionModel.Number52 == String.Empty || actionModel.Number83 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 21");
                }

                if (actionModel.Number22 == true && (actionModel.Number53 == String.Empty || actionModel.Number84 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 22");
                }

                if (actionModel.Number23 == true && (actionModel.Number54 == String.Empty || actionModel.Number85 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 23");
                }

                if (actionModel.Number24 == true && (actionModel.Number55 == String.Empty || actionModel.Number86 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 24");
                }

                if (actionModel.Number25 == true && (actionModel.Number56 == String.Empty || actionModel.Number87 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 25");
                }

                if (actionModel.Number26 == true && (actionModel.Number57 == String.Empty || actionModel.Number88 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 26");
                }

                if (actionModel.Number27 == true && (actionModel.Number58 == String.Empty || actionModel.Number89 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 27");
                }

                if (actionModel.Number28 == true && (actionModel.Number59 == String.Empty || actionModel.Number90 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 28");
                }

                if (actionModel.Number29 == true && (actionModel.Number60 == String.Empty || actionModel.Number91 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 29");
                }

                if (actionModel.Number30 == true && (actionModel.Number61 == String.Empty || actionModel.Number92 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 30");
                }

                if (actionModel.Number31 == true && (actionModel.Number62 == String.Empty || actionModel.Number93 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "Укажите количество и вес в строке 31");
                }
            }
            if (type == (new USR_REQ_UZL_RequestForPeopleAcceptanceItems_View()).GetType())
            {
                string executive;
                if ((actionModel.UserChooseManual1 != null && actionModel.UserChooseManual2 != null && actionModel.UserChooseManual3 != null && actionModel.UserChooseManual4 != null) && ((actionModel.UserChooseManual4 == "") && (actionModel.UserChooseManual1 == "") && (actionModel.UserChooseManual2 == "") && (actionModel.UserChooseManual3 == "")))
                {
                    ModelState.AddModelError(string.Empty, "Неоюходимо заполнить хотя бы одного руководителя");               
                }
                executive = actionModel.UserChooseManual1;
                if (((actionModel.UserChooseManual1 != String.Empty && actionModel.Department1 == String.Empty) || (actionModel.Department1 != String.Empty && actionModel.UserChooseManual1 == String.Empty)) && actionModel.Department1 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО Руководителя 1 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual2 != String.Empty && actionModel.Department2 == String.Empty) || (actionModel.Department2 != String.Empty && actionModel.UserChooseManual2 == String.Empty)) && actionModel.Department2 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО Руководителя 2 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual3 != String.Empty && actionModel.Department3 == String.Empty) || (actionModel.Department3 != String.Empty && actionModel.UserChooseManual3 == String.Empty)) && actionModel.Department3 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО Руководителя 3 и его подразделение должно быть заполнено");
                }

                if (((actionModel.UserChooseManual4 != String.Empty && actionModel.Department4 == String.Empty) || (actionModel.Department4 != String.Empty && actionModel.UserChooseManual4 == String.Empty)) && actionModel.Department4 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО Руководителя 4 и его подразделение должно быть заполнено");
                }

                if ((actionModel.Contact1 == String.Empty || actionModel.UserChooseManual5 == ",") && actionModel.Contact1 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО специалиста 1 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact2 == String.Empty || actionModel.UserChooseManual6 == ",") && actionModel.Contact2 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО специалиста 2 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact3 == String.Empty || actionModel.UserChooseManual7 == ",") && actionModel.Contact3 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО специалиста 3 и его контакты должны быть заполнены");
                }

                if ((actionModel.Contact4 == String.Empty || actionModel.UserChooseManual8 == ",") && actionModel.Contact4 != null)
                {
                    ModelState.AddModelError(string.Empty, "ФИО специалиста 4 и его контакты должны быть заполнены");
                }
            }

            if (type == (new USR_REQ_UBUO_RequestCreateSettlView_View()).GetType())
            {
                if (actionModel.NameSettlView1 != String.Empty && (actionModel.WaySettl1 == String.Empty || actionModel.ReflectedBU1 == String.Empty || actionModel.ReflectedDepartment1 == String.Empty || actionModel.AverageEarnings1 == String.Empty || actionModel.TypeCost1 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 1 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView2 != String.Empty && (actionModel.WaySettl2 == String.Empty || actionModel.ReflectedBU2 == String.Empty || actionModel.ReflectedDepartment2 == String.Empty || actionModel.AverageEarnings2 == String.Empty || actionModel.TypeCost2 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 2 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView3 != String.Empty && (actionModel.WaySettl3 == String.Empty || actionModel.ReflectedBU3 == String.Empty || actionModel.ReflectedDepartment3 == String.Empty || actionModel.AverageEarnings3 == String.Empty || actionModel.TypeCost3 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 3 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView4 != String.Empty && (actionModel.WaySettl4 == String.Empty || actionModel.ReflectedBU4 == String.Empty || actionModel.ReflectedDepartment4 == String.Empty || actionModel.AverageEarnings4 == String.Empty || actionModel.TypeCost4 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 4 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView5 != String.Empty && (actionModel.WaySettl5 == String.Empty || actionModel.ReflectedBU5 == String.Empty || actionModel.ReflectedDepartment5 == String.Empty || actionModel.AverageEarnings5 == String.Empty || actionModel.TypeCost5 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 5 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView6 != String.Empty && (actionModel.WaySettl6 == String.Empty || actionModel.ReflectedBU6 == String.Empty || actionModel.ReflectedDepartment6 == String.Empty || actionModel.AverageEarnings6 == String.Empty || actionModel.TypeCost6 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 6 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView7 != String.Empty && (actionModel.WaySettl7 == String.Empty || actionModel.ReflectedBU7 == String.Empty || actionModel.ReflectedDepartment7 == String.Empty || actionModel.AverageEarnings7 == String.Empty || actionModel.TypeCost7 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 7 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView8 != String.Empty && (actionModel.WaySettl8 == String.Empty || actionModel.ReflectedBU8 == String.Empty || actionModel.ReflectedDepartment8 == String.Empty || actionModel.AverageEarnings8 == String.Empty || actionModel.TypeCost8 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 8 не заполнены все необходимые поля");
                }

                if (actionModel.NameSettlView9 != String.Empty && (actionModel.WaySettl9 == String.Empty || actionModel.ReflectedBU9 == String.Empty || actionModel.ReflectedDepartment9 == String.Empty || actionModel.AverageEarnings9 == String.Empty || actionModel.TypeCost9 == String.Empty))
                {
                    ModelState.AddModelError(string.Empty, "В строке 9 не заполнены все необходимые поля");
                }
            }

            if (type == (new USR_REQ_UBUO_RequestCalcDriveTrip_View()).GetType())
            {
                if (actionModel.FIO1 != null)
                {
                    EmplTripType emplTripType1 = (EmplTripType)actionModel.EmplTripType1;
                    TripDirection tripDirection1 = (TripDirection)actionModel.TripDirection1;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType1 && x.TripDirection == tripDirection1);
                    actionModel.DayRate1 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate1 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO2 != null)
                {
                    EmplTripType emplTripType2 = (EmplTripType)actionModel.EmplTripType2;
                    TripDirection tripDirection2 = (TripDirection)actionModel.TripDirection2;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType2 && x.TripDirection == tripDirection2);
                    actionModel.DayRate2 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate2 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO3 != null)
                {
                    EmplTripType emplTripType3 = (EmplTripType)actionModel.EmplTripType3;
                    TripDirection tripDirection3 = (TripDirection)actionModel.TripDirection3;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType3 && x.TripDirection == tripDirection3);
                    actionModel.DayRate3 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate3 = tripSettingsTable.ResidenceRate;
                }

                if (actionModel.FIO4 != null)
                {
                    EmplTripType emplTripType4 = (EmplTripType)actionModel.EmplTripType4;
                    TripDirection tripDirection4 = (TripDirection)actionModel.TripDirection4;
                    TripSettingsTable tripSettingsTable = _TripSettingsService.FirstOrDefault(x => x.EmplTripType == emplTripType4 && x.TripDirection == tripDirection4);
                    actionModel.DayRate4 = tripSettingsTable.DayRate;
                    actionModel.ResidenceRate4 = tripSettingsTable.ResidenceRate;
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

            DocumentTable document = _DocumentService.Find(documentId);
            document.DocumentText = allStringData;
            _DocumentService.SaveDocumentText(document);

            _SearchService.SaveDomain(new SearchTable { DocumentText = allStringData, DocumentTableId = documentId });
        }
	}
}