using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class BatchController : ApiController
    {
        // GET api/<controller>
        public void Get(int id, string companyId)
        {
            IEmailService _Emailservice = DependencyResolver.Current.GetService<IEmailService>();
            IDocumentService _Documentservice = DependencyResolver.Current.GetService<IDocumentService>();
            IReviewDocLogService _ReviewDocLogService = DependencyResolver.Current.GetService<IReviewDocLogService>();
            IWorkScheduleService _WorkScheduleService = DependencyResolver.Current.GetService<IWorkScheduleService>();
            IAccountService _AccountService = DependencyResolver.Current.GetService<IAccountService>();
            var allDocument = _Documentservice.GetPartial(x => x.CompanyTable.AliasCompanyName == companyId && x.DocumentNum == "RD0000965");

            if (allDocument == null)
                return;

            switch(id)
            {
                case 1:
                    if (_WorkScheduleService.CheckWorkTime(null, DateTime.UtcNow))
                    {
                        var users = _AccountService.GetPartial(x => x.Email != null);
                        List<CheckSLAStatus> checkData = new List<CheckSLAStatus>();

                        foreach (var document in allDocument)
                        {
                            var checkUser = _Documentservice.GetUsersSLAStatus(document, SLAStatusList.Warning);
                            checkData.Add(new CheckSLAStatus(document, checkUser));
                        }

                        foreach (var user in users)
                        {
                            var userDocuments = checkData.Where(x => x.TrackerUsers.Any(a => a.UserId == user.Id)).GroupBy(b => b.DocumentTable).Select(group => group.Key).ToList();
                            if (userDocuments.Count() > 0)
                                _Emailservice.SendSLAWarningEmail(user.Id, userDocuments);
                        }
                    }
                    break;
                case 2:
                    if (_WorkScheduleService.CheckWorkTime(null, DateTime.UtcNow))
                    {
                        var users = _AccountService.GetPartial(x => x.Email != null);
                        List<CheckSLAStatus> checkData = new List<CheckSLAStatus>();

                        foreach (var document in allDocument)
                        {
                            var checkUser = _Documentservice.GetUsersSLAStatus(document, SLAStatusList.Disturbance);
                            checkData.Add(new CheckSLAStatus(document, checkUser));
                        }

                        foreach (var user in users)
                        {
                            var userDocuments = checkData.Where(x => x.TrackerUsers.Any(a => a.UserId == user.Id)).GroupBy(b => b.DocumentTable).Select(group => group.Key).ToList();
                            if (userDocuments.Count() > 0)
                                _Emailservice.SendSLADisturbanceEmail(user.Id, userDocuments);
                        }
                    }
                    break;
                case 3:
                    foreach (var document in allDocument)
                    {
                        if (document.DocumentState == Models.Repository.DocumentState.Closed
                            || document.DocumentState == Models.Repository.DocumentState.Cancelled
                            || document.DocumentState == Models.Repository.DocumentState.Completed
                            || document.DocumentState == Models.Repository.DocumentState.Created)
                        {
                            IEnumerable<ReviewDocLogTable> reviewDocuments = _ReviewDocLogService.GetPartial(x => x.DocumentTableId == document.Id).ToList();

                            if (reviewDocuments != null)
                            {
                                foreach (var reviewTable in reviewDocuments)
                                {
                                    if (reviewTable.CreatedDate <= DateTime.UtcNow.AddDays(-10))
                                    {
                                        reviewTable.isArchive = true;
                                        _ReviewDocLogService.SaveDomain(reviewTable);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    if (_WorkScheduleService.CheckWorkTime(null, DateTime.UtcNow))
                    {
                        var users = _AccountService.GetPartial(x => x.Email != null);
                        List<CheckSLAStatus> checkData = new List<CheckSLAStatus>();

                        foreach (var document in allDocument)
                        {
                            if (document.DocumentState == Models.Repository.DocumentState.Agreement || document.DocumentState == Models.Repository.DocumentState.Execution)
                            {
                                var checkUser = _Documentservice.GetAllUserCurrentStep(document);
                                checkData.Add(new CheckSLAStatus(document, checkUser));
                            }
                        }

                        foreach (var user in users)
                        {
                            var userDocuments = checkData.Where(x => x.TrackerUsers.Any(a => a.UserId == user.Id)).GroupBy(b => b.DocumentTable).Select(group => group.Key).ToList();
                            if (userDocuments.Count() > 0)
                                _Emailservice.SendReminderEmail(user.Id, userDocuments);
                        }
                    }
                    break;
            }
        }
    }

    public class CheckSLAStatus
    {
        public CheckSLAStatus(DocumentTable documentTable, IEnumerable<WFTrackerUsersTable> trackerUsers)
        {
            DocumentTable = documentTable;
            TrackerUsers = trackerUsers;
        }

        public DocumentTable DocumentTable { get; set; }
        public IEnumerable<WFTrackerUsersTable> TrackerUsers { get; set; }
    }
}