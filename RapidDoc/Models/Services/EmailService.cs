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
using Microsoft.AspNet.Identity.EntityFramework;

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
        void SendEmail(EmailParameterTable emailParameter, string[] emailTo, string[] ccTo, string subject, string body);
        void SendInitiatorEmail(Guid documentId);
        void SendExecutorEmail(Guid documentId);
        void SendInitiatorRejectEmail(Guid documentId);
        void SendInitiatorClosedEmail(Guid documentId);
        void SendInitiatorCommentEmail(Guid documentId, string lastComment);
        void SendDelegationEmplEmail(DelegationView delegationView);
        void SendReaderEmail(Guid documentId, List<string> newReader);
        void SendNewExecutorEmail(Guid documentId, string userId);
        void SendNewExecutorEmail(Guid documentId, List<string> userListId);
        void SendSLAWarningEmail(string userId, IEnumerable<DocumentTable> documents);
        void SendSLADisturbanceEmail(string userId, IEnumerable<DocumentTable> documents);
        void SendReminderEmail(ApplicationUser user, List<DocumentTable> documents);
        void SendFailedRoutesAdministrator(List<ReportProcessesView> listProcesses);
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
            _uow.Commit();
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

        public void SendEmail(EmailParameterTable emailParameter, string[] emailTo, string[] ccTo, string subject, string body)
        {
            if (emailTo == null || emailTo.Length == 0)
                return;

            SmtpClient smtpClient = new SmtpClient(emailParameter.SmtpServer, emailParameter.SmtpPort);
            smtpClient.EnableSsl = emailParameter.EnableSsl;
            smtpClient.Timeout = emailParameter.Timeout;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential(emailParameter.UserName, emailParameter.Password);

            MailMessage message = new MailMessage();
            smtpClient.SendCompleted += (s, e) =>
            {
                smtpClient.Dispose();
                message.Dispose();
            };

            message.From = new MailAddress(emailParameter.Email);
            message.Subject = subject == null ? "" : subject;
            message.Body = body == null ? "" : body;
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            foreach (string email in emailTo)
                message.To.Add(email);

            if (emailTo != null && ccTo.Length > 0)
            {
                foreach (string emailCc in ccTo)
                    message.CC.Add(emailCc);
            }

            try
            {
                smtpClient.Send(message);
            }
            catch
            {
                return;
            }
        }

        public void SendInitiatorEmail(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;

            ApplicationUser user = repoUser.GetById(documentTable.ApplicationUserCreatedId);
            if (!String.IsNullOrEmpty(user.Email))
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                string processName = documentTable.ProcessName;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendInitiatorEmail, DocumentText = documentTable.DocumentText }, "emailTemplateDefault");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Вы создали новый документ [{0}]", documentTable.DocumentNum), body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendExecutorEmail(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null || (documentTable.DocumentState != DocumentState.Agreement && documentTable.DocumentState != DocumentState.Execution))
                return;

            var userList = _DocumentService.GetSignUsersDirect(documentTable);

            if (userList.Count < 20)
            {
                foreach (var user in userList)
                {
                    if (!String.IsNullOrEmpty(user.Email))
                    {
                        string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                        EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                        EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                        if (emailParameter == null)
                            return;

                        string processName = documentTable.ProcessName;

                        new Task(() =>
                        {
                            string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml";
                            string razorText = System.IO.File.ReadAllText(absFile);

                            string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                            CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                            Thread.CurrentThread.CurrentCulture = ci;
                            Thread.CurrentThread.CurrentUICulture = ci;
                            string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendExecutorEmail, DocumentText = documentTable.DocumentText }, "emailTemplateDefault");
                            SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Требуется ваша подпись, документ [{0}]", documentTable.DocumentNum), body);
                            ci = CultureInfo.GetCultureInfo(currentLang);
                            Thread.CurrentThread.CurrentCulture = ci;
                            Thread.CurrentThread.CurrentUICulture = ci;
                        }).Start();
                    }
                }
            }
            else
            {
                List<string> emails = userList.Where(x => x.Email != String.Empty).GroupBy(x => x.Email).Select(x => x.Key).ToList();
                if (emails != null && emails.Count > 0)
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";

                    EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                    if (emailParameter == null)
                        return;

                    string processName = documentTable.ProcessName;

                    new Task(() =>
                    {
                        string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicBulkEmailTemplate.cshtml";
                        string razorText = System.IO.File.ReadAllText(absFile);

                        string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                        CultureInfo ci = CultureInfo.GetCultureInfo("ru-RU");
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                        string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, BodyText = UIElementRes.UIElement.SendExecutorEmail, DocumentText = documentTable.DocumentText }, "emailBulkTemplateDefault");
                        SendEmail(emailParameter, emails.ToArray(), new string[] { }, String.Format("Требуется ваша подпись, документ [{0}]", documentTable.DocumentNum), body);
                        ci = CultureInfo.GetCultureInfo(currentLang);
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                    }).Start();
                }
            }
        }

        public void SendInitiatorRejectEmail(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;

            List<string> userList = new List<string>();
            userList.Add(documentTable.ApplicationUserCreatedId);

            if (documentTable.ProcessTable.TableName == "USR_REQ_IT_CTP_IncidentIT")
            {
                var tableModel = _DocumentService.RouteCustomRepository(documentTable.ProcessTable.TableName).GetById(documentTable.RefDocumentId);
                if (tableModel != null)
                {
                    string[] array = tableModel.Users.Split(',');
                    Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                    string[] result = array.Where(a => isGuid.IsMatch(a) == true).ToArray();
                    foreach (var item in result)
                    {
                        Guid emplId = Guid.Parse(item);
                        EmplTable empl = repoEmpl.GetById(emplId);
                        if (empl != null && empl.ApplicationUserId != null && empl.ApplicationUserId != documentTable.ApplicationUserCreatedId)
                            userList.Add(empl.ApplicationUserId);
                    }
                }
            }

            foreach (var userId in userList)
            {
                ApplicationUser user = repoUser.GetById(userId);
                if (!String.IsNullOrEmpty(user.Email))
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                    EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                    EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                    if (emailParameter == null)
                        return;

                    string processName = documentTable.ProcessName;

                    new Task(() =>
                    {
                        string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml";
                        string razorText = System.IO.File.ReadAllText(absFile);

                        string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                        CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                        string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendInitiatorRejectEmail, DocumentText = documentTable.DocumentText }, "emailTemplateDefault");
                        SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Ваш документ [{0}] был отменен", documentTable.DocumentNum), body);
                        ci = CultureInfo.GetCultureInfo(currentLang);
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                    }).Start();
                }
            }
        }

        public void SendInitiatorClosedEmail(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;

            ApplicationUser user = repoUser.GetById(documentTable.ApplicationUserCreatedId);
            if (!String.IsNullOrEmpty(user.Email))
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                string processName = documentTable.ProcessName;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendInitiatorClosedEmail, DocumentText = documentTable.DocumentText }, "emailTemplateDefault");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Ваш документ [{0}] закрыт", documentTable.DocumentNum), body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendInitiatorCommentEmail(Guid documentId, string lastComment)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null || documentTable.DocType != DocumentType.Request)
                return;

            var users = repoUser.FindAll(x => x.Id == documentTable.ApplicationUserCreatedId).ToList();

            var currentReaders = _DocumentReaderService.GetPartial(x => x.DocumentTableId == documentId).ToList();
            foreach (var reader in currentReaders)
                users.Add(repoUser.GetById(reader.UserId));

            var signUsers = _DocumentService.GetSignUsersDirect(documentTable);
            foreach (var signUser in signUsers)
            {
                if (users.Any(x => x.Id == signUser.Id))
                    continue;
                else
                    users.Add(signUser);
            }

            foreach (var user in users)
            {
                if (!String.IsNullOrEmpty(user.Email) && user.UserName != HttpContext.Current.User.Identity.Name)
                {
                    string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                    EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                    EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                    if (emailParameter == null)
                        return;

                    string processName = documentTable.ProcessName;

                    new Task(() =>
                    {
                        string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\CommentEmailTemplate.cshtml";
                        string razorText = System.IO.File.ReadAllText(absFile);

                        string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                        CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                        string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendInitiatorCommentEmail, LastComment = lastComment, DocumentText = documentTable.DocumentText }, "emailTemplateComment");
                        SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Новый комментарий в документе [{0}]", documentTable.DocumentNum), body);
                        ci = CultureInfo.GetCultureInfo(currentLang);
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                    }).Start();
                }
            }
        }

        public void SendDelegationEmplEmail(DelegationView delegationView)
        {
            EmplTable emplTableFrom = repoEmpl.GetById(delegationView.EmplTableFromId);
            EmplTable emplTableTo = repoEmpl.GetById(delegationView.EmplTableToId);
            ApplicationUser user = repoUser.GetById(emplTableTo.ApplicationUserId);

            if (!String.IsNullOrEmpty(user.Email))
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString();

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\DelegationEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentUri = documentUri, EmplNameTo = emplTableTo.FullName, EmplNameFrom = emplTableFrom.FullName, BodyText = UIElementRes.UIElement.SendDelegationEmplEmail }, "emailTemplateDelegation");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, "На вас настроенно делегирование", body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendReaderEmail(Guid documentId, List<string> newReader)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;

            List<string> emails = repoUser.FindAll(x => newReader.Contains(x.Id) && x.Email != String.Empty).GroupBy(x => x.Email).Select(x => x.Key).ToList();

            if (emails != null && emails.Count > 0)
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                string processName = documentTable.ProcessName;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicBulkEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo("ru-RU");
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, BodyText = UIElementRes.UIElement.SendReaderEmail, DocumentText = documentTable.DocumentText }, "emailBulkTemplateDefault");
                    SendEmail(emailParameter, emails.ToArray(), new string[] { }, String.Format("Вас добавили читателем, документ [{0}]", documentTable.DocumentNum), body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendNewExecutorEmail(Guid documentId, string userId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;
            ApplicationUser user = repoUser.GetById(userId);

            if (!String.IsNullOrEmpty(user.Email))
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";
                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                string processName = documentTable.ProcessName;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendExecutorEmail, DocumentText = documentTable.DocumentText }, "emailTemplateDefault");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, String.Format("Требуется ваша подпись, документ [{0}]", documentTable.DocumentNum), body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendNewExecutorEmail(Guid documentId, List<string> userListId)
        {
            var documentTable = _DocumentService.Find(documentId);
            if (documentTable == null)
                return;

            List<string> emails = repoUser.FindAll(x => userListId.Contains(x.Id) && x.Email != String.Empty).GroupBy(x => x.Email).Select(x => x.Key).ToList();

            if (emails != null && emails.Count > 0)
            {
                string documentUri = "http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true";

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                string processName = documentTable.ProcessName;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\BasicBulkEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo("ru-RU");
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentNum = String.Format("{0} - {1}", documentTable.DocumentNum, processName), DocumentUri = documentUri, BodyText = UIElementRes.UIElement.SendExecutorEmail, DocumentText = documentTable.DocumentText }, "emailBulkTemplateDefault");
                    SendEmail(emailParameter, emails.ToArray(), new string[] { }, String.Format("Требуется ваша подпись, документ [{0}]", documentTable.DocumentNum), body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendSLAWarningEmail(string userId, IEnumerable<DocumentTable> documents)
        {
            ApplicationUser user = repoUser.GetById(userId);
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (!String.IsNullOrEmpty(user.Email))
            {
                int num = 0;
                foreach (var documentTable in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true");
                    documentNums.Add(documentTable.DocumentNum + " - " + documentTable.ProcessName);
                    documentText.Add(documentTable.DocumentText);
                }

                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentUri = "", DocumentUris = documentUrls.ToArray(), DocumentNums = documentNums.ToArray(), documentText = documentText.ToArray(), EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendSLAWarningEmail }, "emailTemplateSLAStatus");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, "Срок исполнения подходит к концу", body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendSLADisturbanceEmail(string userId, IEnumerable<DocumentTable> documents)
        {
            ApplicationUser user = repoUser.GetById(userId);
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (!String.IsNullOrEmpty(user.Email))
            {
                int num = 0;
                foreach (var documentTable in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true");
                    documentNums.Add(documentTable.DocumentNum + " - " + documentTable.ProcessName);
                    documentText.Add(documentTable.DocumentText);
                }

                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentUri = "", DocumentUris = documentUrls.ToArray(), DocumentNums = documentNums.ToArray(), documentText = documentText.ToArray(), EmplName = emplTable.FullName, BodyText = UIElementRes.UIElement.SendSLADisturbanceEmail }, "emailTemplateSLAStatus");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, "Исполнение по документам просрочено", body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendReminderEmail(ApplicationUser user, List<DocumentTable> documents)
        {
            List<string> documentUrls = new List<string>();
            List<string> documentNums = new List<string>();
            List<string> documentText = new List<string>();

            if (!String.IsNullOrEmpty(user.Email))
            {
                int num = 0;
                foreach (var documentTable in documents)
                {
                    num++;
                    documentUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + documentTable.CompanyTable.AliasCompanyName + "/Document/ShowDocument/" + documentTable.Id + "?isAfterView=true");
                    documentNums.Add(documentTable.DocumentNum + " - " + documentTable.ProcessName);

                    if (!String.IsNullOrEmpty(documentTable.DocumentText) && documentTable.DocumentText.Length > 80)
                        documentText.Add(documentTable.DocumentText.Substring(0, 80) + "...");
                    else
                        documentText.Add(documentTable.DocumentText);
                }

                EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                if (emailParameter == null)
                    return;

                new Task(() =>
                {
                    string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\SLAEmailTemplate.cshtml";
                    string razorText = System.IO.File.ReadAllText(absFile);

                    string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                    CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                    string body = Razor.Parse(razorText, new { DocumentUri = "", DocumentUris = documentUrls.ToArray(), DocumentNums = documentNums.ToArray(), documentText = documentText.ToArray(), EmplName = emplTable.FullName, BodyText = "Документы на подписи" }, "emailTemplateSLAStatus");
                    SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, "У вас на подписи находятся следующие документы", body);
                    ci = CultureInfo.GetCultureInfo(currentLang);
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }).Start();
            }
        }

        public void SendFailedRoutesAdministrator(List<ReportProcessesView> listProcesses)
        {
            List<string> processUrls = new List<string>();
            List<string> stageNames = new List<string>();
            List<string> filterTexts = new List<string>();

            RoleManager<ApplicationRole> RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_uow.GetDbContext<ApplicationDbContext>()));

            if (RoleManager.RoleExists("SetupAdministrator"))
            {
                foreach (ReportProcessesView reportProcess in listProcesses)
                {
                    processUrls.Add("http://" + ConfigurationManager.AppSettings.Get("WebSiteUrl").ToString() + "/" + reportProcess.Process.CompanyTable.AliasCompanyName + "/Process/Edit/" + reportProcess.Process.Id);
                    stageNames.Add(reportProcess.StageName + " - " + reportProcess.Process.TableName);
                    filterTexts.Add(reportProcess.FilterText);
                }
            
                var names = RoleManager.FindByName("SetupAdministrator").Users;
                if (names != null && names.Count() > 0)
                {
                    foreach (IdentityUserRole name in names)
                    {
                        ApplicationUser user = repoUser.Find(x => (x.UserName == name.UserId || x.Id == name.UserId) && x.Enable == true);
                        if (!String.IsNullOrEmpty(user.Email))
                        {
                            EmplTable emplTable = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.Enable == true);

                            EmailParameterTable emailParameter = FirstOrDefault(x => x.SmtpServer != String.Empty);
                            if (emailParameter == null)
                                return;

                            new Task(() =>
                            {
                                string absFile = HostingEnvironment.ApplicationPhysicalPath + @"Views\\EmailTemplate\\RoutEmailTemplate.cshtml";
                                string razorText = System.IO.File.ReadAllText(absFile);

                                string currentLang = Thread.CurrentThread.CurrentCulture.Name;
                                CultureInfo ci = CultureInfo.GetCultureInfo(user.Lang);
                                Thread.CurrentThread.CurrentCulture = ci;
                                Thread.CurrentThread.CurrentUICulture = ci;
                                string body = Razor.Parse(razorText, new { DocumentUri = "", DocumentUris = processUrls.ToArray(), DocumentNums = stageNames.ToArray(), documentText = filterTexts.ToArray(), EmplName = emplTable.FullName, BodyText = "У вас несколько ошибочных маршрутов" }, "emailTemplateRoutes");
                                SendEmail(emailParameter, new string[] { user.Email }, new string[] { }, "Маршруты процессов на исправление", body);
                                ci = CultureInfo.GetCultureInfo(currentLang);
                                Thread.CurrentThread.CurrentCulture = ci;
                                Thread.CurrentThread.CurrentUICulture = ci;
                            }).Start();
                        }   
                    }
                }              
            }
        }
    }
}