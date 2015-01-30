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
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentTable> GetAll();
        IEnumerable<CommentTable> GetPartial(Expression<Func<CommentTable, bool>> predicate);
        IEnumerable<CommentView> GetPartialView(Expression<Func<CommentTable, bool>> predicate);
        CommentTable FirstOrDefault(Expression<Func<CommentTable, bool>> predicate);
        bool Contains(Expression<Func<CommentTable, bool>> predicate);
        void SaveDomain(CommentTable domainTable);
        void Delete(Guid Id);
        CommentTable Find(Guid id);
        void DeleteAll(Guid documenId);
    }

    public class CommentService : ICommentService
    {
        private IRepository<CommentTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;

        public CommentService(IUnitOfWork uow, IAccountService accountService, IEmplService emplService, ISystemService systemService)
        {
            _uow = uow;
            repo = uow.GetRepository<CommentTable>();
            _AccountService = accountService;
            _EmplService = emplService;
            _SystemService = systemService;
        }
        public IEnumerable<CommentTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<CommentTable> GetPartial(Expression<Func<CommentTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<CommentView> GetPartialView(Expression<Func<CommentTable, bool>> predicate)
        {
            var comments = GetPartial(predicate);
            List<CommentView> commentsView = new List<CommentView>();

            if (comments != null)
            {
                ApplicationUser user = _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());

                foreach (var comment in comments)
                {
                    EmplTable empl = _EmplService.GetEmployer(comment.ApplicationUserCreatedId, comment.CompanyTableId);
                    if (empl != null)
                        commentsView.Add(new CommentView { Id = comment.Id, Comment = comment.Comment, CreatedDate = _SystemService.ConvertDateTimeToLocal(user, comment.CreatedDate), EmplName = empl.FullName, TitleName = empl.TitleName });
                    else
                        commentsView.Add(new CommentView { Id = comment.Id, Comment = comment.Comment, CreatedDate = _SystemService.ConvertDateTimeToLocal(user, comment.CreatedDate), EmplName = user.UserName, TitleName = "" });
                }
            }

            return commentsView;
        }
        public CommentTable FirstOrDefault(Expression<Func<CommentTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public bool Contains(Expression<Func<CommentTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }
        public void SaveDomain(CommentTable domainTable)
        {
            ApplicationUser user = _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());
            domainTable.CreatedDate = DateTime.UtcNow;
            domainTable.ModifiedDate = domainTable.CreatedDate;
            domainTable.ApplicationUserCreatedId = user.Id;
            domainTable.ApplicationUserModifiedId = user.Id;
            domainTable.CompanyTableId = user.CompanyTableId;
            repo.Add(domainTable);
            _uow.Save();
        }
        public void Delete(Guid Id)
        {
            repo.Delete(x => x.Id == Id);
            _uow.Save();
        }
        public void DeleteAll(Guid documentId)
        {
            repo.Delete(x => x.DocumentTableId == documentId);
            _uow.Save();
        }
        public CommentTable Find(Guid id)
        {
            return repo.GetById(id);
        }
    }
}