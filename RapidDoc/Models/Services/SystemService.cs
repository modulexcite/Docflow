using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RapidDoc.Models.Services
{
    public interface ISystemService
    {
        DateTime ConvertDateTimeToLocal(string UserId, DateTime value);
        DateTime ConvertDateTimeToLocal(ApplicationUser userTable, DateTime value);
        bool IsGUID(string expression);
    }

    public class SystemService : ISystemService
    {
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public SystemService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            _AccountService = accountService;
        }

        public DateTime ConvertDateTimeToLocal(string UserId, DateTime value)
        {
            ApplicationUser userTable = _AccountService.Find(UserId);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTable.TimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(value, timeZoneInfo);
        }

        public DateTime ConvertDateTimeToLocal(ApplicationUser userTable, DateTime value)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(userTable.TimeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(value, timeZoneInfo);
        }

        public bool IsGUID(string expression)
        {
            if (expression != null)
            {
                Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

                return isGuid.IsMatch(expression);
            }
            return false;
        }

    }
}