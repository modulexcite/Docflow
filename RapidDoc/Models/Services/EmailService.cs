using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using System.Text;
using RazorEngine;
using System.Web.Hosting;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace RapidDoc.Models.Services
{
    public interface IEmailService
    {
        EmailParameterTable FirstOrDefault(Expression<Func<EmailParameterTable, bool>> predicate);
        EmailParameterView FirstOrDefaultView(Expression<Func<EmailParameterTable, bool>> predicate);
        void Save(EmailParameterView viewTable);
        void SaveDomain(EmailParameterTable domainTable);
        EmailParameterTable Find(Guid id);
        void InitializeMailParameter();
        string SendEmail(EmailParameterView model, string[] emailTo, string[] ccTo, string subject, string body);
        void SendInitiatorEmail(Guid documentId);
        void SendExecutorEmail(Guid documentId);
        void SendInitiatorRejectEmail(Guid documentId);
        void SendInitiatorClosedEmail(Guid documentId);
        void SendInitiatorCommentEmail(Guid documentId, string lastComment);
        void SendDelegationEmplEmail(DelegationView delegationView);
        void SendReaderEmail(Guid documentId, List<string> newReader);
        void SendNewExecutorEmail(Guid documentId, string userId);
        void SendSLAWarningEmail(string userId, IEnumerable<DocumentTable> documents);
        void SendSLADisturbanceEmail(string userId, IEnumerable<DocumentTable> documents);
        void SendReminderEmail(string userId, IEnumerable<DocumentTable> documents);
    }

    public class EmailService : IEmailService
    {
        private IRepository<EmailParameterTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IRepository<EmplTable> repoEmpl;
        private IUnitOfWork _uow;
        private readonly IDocumentService _DocumentService;
        private readonly IDocumentReaderService _DocumentReaderService;

        public EmailService(IUnitOfWork uow, IDocumentService documentService, IDocumentReaderService documentReaderService)
        {
            _uow = uow;
            repo = uow.GetRepository<EmailParameterTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
            repoEmpl = uow.GetRepository<EmplTable>();
            _DocumentService = documentService;
            _DocumentReaderService = documentReaderService;
        }

        public EmailParameterTable FirstOrDefault(Expression<Func<EmailParameterTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public EmailParameterView FirstOrDefaultView(Expression<Func<EmailParameterTable, bool>> predicate)
        {
            return Mapper.Map<EmailParameterTable, EmailParameterView>(FirstOrDefault(predicate));
        }

        public void Save(EmailParameterView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new EmailParameterTable();
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
            else
            {
                var domainTable = Find(viewTable.Id ?? Guid.Empty);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }

        public void SaveDomain(EmailParameterTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = userId;
                domainTable.ApplicationUserModifiedId = userId;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = userId;
                repo.Update(domainTable);
            }
            _uow.Save();
        }

        public EmailParameterTable Find(Guid id)
        {
            return repo.GetById(id);
        }

        public void InitializeMailParameter()
        {
            EmailParameterTable domainTable = new EmailParameterTable();
            domainTable.SmtpServer = "SERVER NAME";
            domainTable.Email = "user@company.com";
            domainTable.UserName = "username";
            SaveDomain(domainTable);
        }

        public string SendEmail(EmailParameterView model, string[] emailTo, string[] ccTo, string subject, string body)
        {
            if (emailTo == null || emailTo.Length == 0)
            {
                return "Email To Address Empty";
            }

            //var model = FirstOrDefaultView(x => x.SmtpServer != String.Empty);

            if(model == null)
            {
                return "No parameters";
            }

            SmtpClient smtpClient = new SmtpClient(model.SmtpServer, model.SmtpPort);
            smtpClient.EnableSsl = model.EnableSsl;
            smtpClient.Timeout = model.Timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential(model.UserName, model.Password);

            MailMessage message = new MailMessage();
            smtpClient.SendCompleted += (s, e) =>
            {
                smtpClient.Dispose();
                message.Dispose();
            };


            message.From = new MailAddress(model.Email);
            message.Subject = subject == null ? "" : subject;
            message.Body = body == null ? "" : body;
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            foreach (string email in emailTo)
            {
                message.To.Add(email);
            }

            if (emailTo != null && ccTo.Length > 0)
            {
                foreach (string emailCc in ccTo)
                {
                    message.CC.Add(emailCc);
                }
            }

            try
            {
                smtpClient.Send(message);
                return "Email Send successFully";
            }
            catch
            {
                return "Email Send failed";
            }
        }

        private void CreateMessange(EmailTemplateType type, DocumentTable docuTable, ApplicationUser userTable, string templateName, string documentUri, string bodyText, string subject, string[] parameters, string[] parameters2 = null, string[] parameters3 = null)
        {
            var emplTable = repoEmpl.Find(x => x.ApplicationUserId == userTable.Id);

            if (emplTable == null)
                return;

            string processName = String.Empty;

            if (docuTable != null)
            {
                processName = docuTable.ProcessTable.ProcessName;
            }

            EmailParameterView emailParameter = FirstOrDefaultView(x => x.SmtpServer != String.Empty);

            new Task(() =>
            {
                string absFile = HostingEnvironment.ApplicationPhysicalPath + templateName;
                string razorText = System.IO.File.ReadAllText(absFile);

                string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                CultureInfo ci = CultureInfo.GetCultureInfo(userTable.Lang);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
                string body = String.Empty;

                switch (type)
                {
                    case EmailTemplateType.Default:
                        body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", docuTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = bodyText, DocumentText = parameters2[0] });
                        break;
                    case EmailTemplateType.Comment:
                        body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", docuTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = bodyText, LastComment = parameters[0], DocumentText = parameters2[0] });
                        break;

                    case EmailTemplateType.Delegation:
                        body = Razor.Parse(razorText, new { DocumentUri = documentUri, EmplNameTo = parameters[0], EmplNameFrom = parameters[1], BodyText = bodyText });
                        break;

                    case EmailTemplateType.SLAStatus:
                        body = Razor.Parse(razorText, new { DocumentUri = "", DocumentUris = parameters, DocumentNums = parameters2, documentText = parameters3, EmplName = emplTable.FullName, BodyText = bodyText });
                        break;
                }
                SendEmail(emailParameter, new string[] { userTable.Email }, new string[] { }, subject, body);

                ci = CultureInfo.GetCultureInfo(currentLang);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }).Start();
        }

        public void SendInitiatorEmail(Guid documentId)
        {
            var docuTable = _DocumentService.Find(documentId);

            if (docuTable != null)
            {
                ApplicationUser userTable = repoUser.GetById(docuTable.ApplicationUserCreatedId);
                if (userTable.Email != String.Empty)
                {
                    string documentUri = HttpContext.Current.Request.UrlReferrer.AbsoluteUri.Replace("Create", "ShowDocument");
                    documentUri = documentUri.Substring(0, documentUri.Length - 36);
                    documentUri = documentUri + docuTable.Id.ToString();

                    CreateMessange(EmailTemplateType.Default, docuTable, userTable, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendInitiatorEmail, String.Format("Вы создали новый документ [{0}]", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
                }
            }
        }

        public void SendExecutorEmail(Guid documentId)
        {
            var docuTable = _DocumentService.Find(documentId);

            if(docuTable != null && docuTable.DocumentState == DocumentState.Agreement)
            {
                var userList = _DocumentService.GetSignUsersDirect(docuTable);

                foreach(var user in userList)
                {
                    if (user.Email != String.Empty)
                    {
                        string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                        CreateMessange(EmailTemplateType.Default, docuTable, user, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendExecutorEmail, String.Format("Требуется ваша подпись, документ [{0}]", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
                    }
                }
            }
        }

        public void SendInitiatorRejectEmail(Guid documentId)
        {
            var docuTable = _DocumentService.Find(documentId);

            if (docuTable != null)
            {
                List<string> userList = new List<string>();
                userList.Add(docuTable.ApplicationUserCreatedId);

                if(docuTable.ProcessTable.TableName == "USR_REQ_IT_CTP_IncidentIT")
                {
                    var tableModel = _DocumentService.RouteCustomRepository(docuTable.ProcessTable.TableName).GetById(docuTable.RefDocumentId);
                    if (tableModel != null)
                    {
                        string[] array = tableModel.Users.Split(',');
                        Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                        string[] result = array.Where(a => isGuid.IsMatch(a) == true).ToArray();
                        foreach (var item in result)
                        {
                            Guid emplId = Guid.Parse(item);
                            EmplTable empl = repoEmpl.GetById(emplId);
                            if (empl != null && empl.ApplicationUserId != null && empl.ApplicationUserId != docuTable.ApplicationUserCreatedId)
                            {
                                userList.Add(empl.ApplicationUserId);
                            }
                        }
                    }
                }

                foreach (var userId in userList)
                {
                    ApplicationUser userTable = repoUser.GetById(userId);
                    if (userTable.Email != String.Empty)
                    {
                        string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                        CreateMessange(EmailTemplateType.Default, docuTable, userTable, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendInitiatorRejectEmail, String.Format("Ваш документ [{0}] был отменен", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
                    }
                }
            }
        }

        public void SendInitiatorClosedEmail(Guid documentId)
        {
            var docuTable = _DocumentService.Find(documentId);

            if (docuTable != null)
            {
                ApplicationUser userTable = repoUser.GetById(docuTable.ApplicationUserCreatedId);
                if (userTable.Email != String.Empty)
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                    CreateMessange(EmailTemplateType.Default, docuTable, userTable, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendInitiatorClosedEmail, String.Format("Ваш документ [{0}] закрыт", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
                }
            }
        }

        public void SendInitiatorCommentEmail(Guid documentId, string lastComment)
        {
            var docuTable = _DocumentService.Find(documentId);

            var currentReaders = _DocumentReaderService.GetPartial(x => x.DocumentTableId == documentId).ToList();
            var users = repoUser.FindAll(x => x.Id == docuTable.ApplicationUserCreatedId).ToList();

            foreach (var reader in currentReaders)
            {
                users.Add(repoUser.GetById(reader.UserId));
            }

            var signUsers = _DocumentService.GetSignUsersDirect(docuTable);
            foreach (var signUser in signUsers)
            {
                if (users.Any(x => x.Id == signUser.Id))
                    continue;
                else
                    users.Add(signUser);
            }


            if (docuTable != null)
            {
                //ApplicationUser userTable = _AccountService.Find(docuTable.ApplicationUserCreatedId);
                foreach (var userTable in users)
                {
                    if (userTable.Email != String.Empty && userTable.UserName != HttpContext.Current.User.Identity.Name)
                    {
                        string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                        CreateMessange(EmailTemplateType.Comment, docuTable, userTable, @"Views\\EmailTemplate\\CommentEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendInitiatorCommentEmail, String.Format("Новый комментарий в документе [{0}]", docuTable.DocumentNum), new string[] { lastComment }, new string[] { docuTable.DocumentText });
                    }
                }
            }
        }

        public void SendDelegationEmplEmail(DelegationView delegationView)
        {
            if (delegationView != null)
            {
                var emplTableFrom = repoEmpl.GetById(delegationView.EmplTableFromId);
                var emplTableTo = repoEmpl.GetById(delegationView.EmplTableToId);
                ApplicationUser userTable = repoUser.GetById(emplTableTo.ApplicationUserId);

                if (userTable.Email != String.Empty)
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString();
                    CreateMessange(EmailTemplateType.Delegation, null, userTable, @"Views\\EmailTemplate\\DelegationEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendDelegationEmplEmail, "На вас настроенно делегирование", new string[] { emplTableTo.FullName, emplTableFrom.FullName });
                }
            }
        }

        public void SendReaderEmail(Guid documentId, List<string> newReader)
        {
            var docuTable = _DocumentService.Find(documentId);

            foreach (var userId in newReader)
            {
                ApplicationUser userTable = repoUser.GetById(userId);

                if (userTable.Email != String.Empty)
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                    CreateMessange(EmailTemplateType.Default, docuTable, userTable, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendReaderEmail, String.Format("Вас добавили читателем, документ [{0}]", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
                }
            }
        }

        public void SendNewExecutorEmail(Guid documentId, string userId)
        {
            var docuTable = _DocumentService.Find(documentId);
            ApplicationUser userTable = repoUser.GetById(userId);

            if (userTable.Email != String.Empty)
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + docuTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + docuTable.Id + "?isAfterView=true";
                CreateMessange(EmailTemplateType.Default, docuTable, userTable, @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml", documentUri, UIElementRes.UIElement.SendExecutorEmail, String.Format("Требуется ваша подпись, документ [{0}]", docuTable.DocumentNum), new string[] { }, new string[] { docuTable.DocumentText });
            }
        }

        public void SendSLAWarningEmail(string userId, IEnumerable<DocumentTable> documents)
        {
            ApplicationUser userTable = repoUser.GetById(userId);
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (userTable.Email != String.Empty)
            {
                int num = 0;
                foreach (var document in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + document.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + document.Id + "?isAfterView=true");
                    documentNums.Add(document.DocumentNum + " - " + document.ProcessName);
                    documentText.Add(document.DocumentText);
                }

                CreateMessange(EmailTemplateType.SLAStatus, null, userTable, @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml", null, UIElementRes.UIElement.SendSLAWarningEmail, String.Format("Срок исполнения подходит к концу"), documentUrls.ToArray(), documentNums.ToArray(), documentText.ToArray());
            }
        }

        public void SendSLADisturbanceEmail(string userId, IEnumerable<DocumentTable> documents)
        {
            ApplicationUser userTable = repoUser.GetById(userId);
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (userTable.Email != String.Empty)
            {
                int num = 0;
                foreach (var document in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + document.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + document.Id + "?isAfterView=true");
                    documentNums.Add(document.DocumentNum + " - " + document.ProcessName);
                    documentText.Add(document.DocumentText);
                }

                CreateMessange(EmailTemplateType.SLAStatus, null, userTable, @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml", null, UIElementRes.UIElement.SendSLADisturbanceEmail, String.Format("Исполнение по документам просрочено"), documentUrls.ToArray(), documentNums.ToArray(), documentText.ToArray());
            }
        }

        public void SendReminderEmail(string userId, IEnumerable<DocumentTable> documents)
        {
            ApplicationUser userTable = repoUser.GetById(userId);
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (userTable.Email != String.Empty)
            {
                int num = 0;
                foreach (var document in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + document.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + document.Id + "?isAfterView=true");
                    documentNums.Add(document.DocumentNum + " - " + document.ProcessName);

                    if (!String.IsNullOrEmpty(document.DocumentText) && document.DocumentText.Length > 80)
                        documentText.Add(document.DocumentText.Substring(0, 80) + "...");
                    else
                        documentText.Add(document.DocumentText);
                }

                CreateMessange(EmailTemplateType.SLAStatus, null, userTable, @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml", null, "У вас на подписи находятся следующие документы", String.Format("Документы на подписи"), documentUrls.ToArray(), documentNums.ToArray(), documentText.ToArray());
            }
        }
    }
}