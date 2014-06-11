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
            var allDocument = _Documentservice.GetPartial(x => x.CompanyTable.AliasCompanyName == companyId);

            if (allDocument == null)
                return;

            switch(id)
            {
                case 1:
                    foreach (var document in allDocument)
                    {
                        var users = _Documentservice.GetUsersSLAStatus(document, SLAStatusList.Warning);
                        foreach(var user in users)
                        {
                            _Emailservice.SendSLAWarningEmail(document, user.UserId);
                        }
                    }
                    break;
                case 2:
                    foreach (var document in allDocument)
                    {
                        var users = _Documentservice.GetUsersSLAStatus(document, SLAStatusList.Disturbance);
                        foreach (var user in users)
                        {
                            _Emailservice.SendSLADisturbanceEmail(document, user.UserId);
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
                            IEnumerable<ReviewDocLogTable> reviewDocuments = _ReviewDocLogService.GetPartial(x => x.DocumentTableId == document.Id);

                            if (reviewDocuments != null)
                            {
                                foreach (var reviewTable in reviewDocuments)
                                {
                                    if (reviewTable.CreatedDate <= DateTime.UtcNow.AddMonths(-2))
                                    {
                                        reviewTable.isArchive = true;
                                        _ReviewDocLogService.SaveDomain(reviewTable);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}