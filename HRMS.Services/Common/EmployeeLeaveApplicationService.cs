using AutoMapper;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.Notification;
using HRMS.Core.Utilities.Auth;
using HRMS.Core.Utilities.General;
using HRMS.DBL.Stores;
using HRMS.Services.Common.Builder;
using HRMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using HRMS.DBL.Extensions;

namespace HRMS.Services.Common
{
    public class EmployeeLeaveApplicationService : IEmployeeLeaveApplicationService
    {
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly ISystemFlagService _systemFlagService;
        private readonly INotificationService<NotificationTemplate> _notificationService;

        private readonly EmployeeLeaveStore _employeeLeaveStore;
        private readonly EmployeeLeaveApplicationStore _employeeLeaveApplicationStore;
        private readonly EmployeeContractStore _employeeContractStore;
        private readonly EmployeeStore _employeeStore;
        private readonly NotificationStore _notificationStore;

        public EmployeeLeaveApplicationService(IMapper mapper, IUserContextAccessor userContextAccessor, EmployeeLeaveApplicationStore employeeLeaveApplicationStore, IGeneralUtilities generalUtilities, IEmployeeService employeeService,
            EmployeeContractStore employeeContractStore, ISystemFlagService systemFlagService, INotificationService<NotificationTemplate> notificationService, EmployeeStore employeeStore, NotificationStore notificationStore, EmployeeLeaveStore employeeLeaveStore)
        {
            _employeeLeaveApplicationStore = employeeLeaveApplicationStore;
            _userContextAccessor = userContextAccessor;
            _employeeContractStore = employeeContractStore;
            _systemFlagService = systemFlagService;
            _notificationService = notificationService;
            _employeeStore = employeeStore;
            _notificationStore = notificationStore;
            _employeeLeaveStore = employeeLeaveStore;
        }

        #region Leave Application
        public async Task<Guid> AddorUpdateEmployeeLeaveApplication(EmployeeLeaveApplicationRequestModel model)
        {
            var result = await _employeeLeaveApplicationStore.AddorUpdateEmployeeLeaveApplication(model);
            if (result != Guid.Empty)
            {
                var message = _notificationService.GetTemplateText("Leave", NotificationTypes.InApp);
                var employee = await _employeeStore.GetEmployeeDetails(model.EmployeeCode);
                message = message.Replace("%Employee%", $"{employee?.Employee.FirstName} {employee?.Employee.LastName} ({employee?.Employee.EmployeeCode})").Replace("%TOTALDAYSHOURS%", $"{model.NoOfDays} Hours").Replace("%DATES%", $"{model.LeaveFromDate.Value.ToString("MMMM dd, yyyy")} - {model.LeaveToDate.Value.ToString("MMMM dd, yyyy")}");

                var higerUps = await _employeeStore.GetEmployeeInformationByDesignation(new List<int?> { (int)DesignationTypes.HRExecutive, (int)DesignationTypes.Director }, true, null, null);
                var managers = higerUps?.Select(x => x.EmployeeId).ToList();
                managers.AddRange(model.ProjectManagerIds);

                var notifications = new List<NotificationModel>();
                foreach (var item in managers?.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList())
                {
                    notifications.Add(new NotificationModel
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = item,
                        HasRead = false,
                        Description = message,
                        Title = "Leave Request",
                        Type = NotificationTypes.InApp.GetEnumDescriptionAttribute()
                    });
                }
                await _notificationStore.AddNotifications(notifications);
            }
            return result;
        }
        public async Task<EmployeeLeaveApplicationListModel> GetEmployeeLeaveApplications(EmployeeLeaveApplicationFilterModel filter, int? employeeId, RoleTypes? userRole)
        {
            var data = await _employeeLeaveApplicationStore.GetEmployeeLeaveApplications(filter, employeeId);
            var records = data?.ToEmployeeLeaveApplications(false).ToList();

            if (userRole == RoleTypes.Manager)
            {
                var requested = await _employeeLeaveApplicationStore.GetRequestedEmployeeLeaveApplications(filter, employeeId, userRole);
                var t = requested.ToEmployeeLeaveApplications(true).ToList();
                records.AddRange(t);
            }

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = records.Count();
                records = records.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeLeaveApplicationListModel
            {
                Applications = records,
                TotalRecords = totalRecords,
            };
        }
        public async Task<bool> DeleteEmployeeLeaveApplications(IEnumerable<Guid> ids)
        {
            return await _employeeLeaveApplicationStore.DeleteEmployeeLeaveApplications(ids);
        }
        public async Task<bool> SetEmployeeLeaveApplicationsStatus(EmployeeLeaveApplicationStatusRequestModel model)
        {
            var result = await _employeeLeaveApplicationStore.SetEmployeeLeaveApplicationsStatus(model);
            await _employeeLeaveStore.GenerateLeaveTransactions(result?.transactions, "");
            await SendSlackNotification(result?.reponse?.EmployeeCode, model);
            await SendEmailNotification(result?.reponse?.EmployeeCode, model);
            await SendInAppNotification(result?.reponse?.EmployeeCode, model);
            return (result?.reponse?.Status ?? false);
        }
        public async Task<EmployeeLeaveApplicationModel> GetEmployeeLeaveApplications(Guid id)
        {
            var data = await _employeeLeaveApplicationStore.GetEmployeeLeaveApplications(id);
            return data.ToEmployeeLeaveApplications().FirstOrDefault();
        }
        public async Task<int?> GetLeaveApplicationSandwichDays(SandwichDateRequestModel model)
        {
            return await _employeeLeaveStore.GetLeaveApplicationSandwichDays(model);
        }
        public async Task<int?> GetTotalRunningContract(string employeeCode)
        {
            return await _employeeContractStore.GetTotalRunningContract(employeeCode);
        }
        #endregion Leave Application
        #region Leave Comment
        public async Task<Guid> AddorUpdateEmployeeLeaveApplicationComment(EmployeeLeaveApplicationCommentRequestModel model)
        {
            return await _employeeLeaveApplicationStore.AddorUpdateEmployeeLeaveApplicationComment(model, _userContextAccessor.EmployeeId);
        }
        public async Task<EmployeeLeaveApplicationCommentListModel> GetEmployeeLeaveApplicationComments(EmployeeLeaveApplicationCommentFilterModel filter)
        {
            var data = await _employeeLeaveApplicationStore.GetEmployeeLeaveApplicationComments(filter);

            int totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = data.Count();
                data = data.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize);
            }

            var records = data.ToEmployeeLeaveApplicationComments().ToList();
            return new EmployeeLeaveApplicationCommentListModel
            {
                Comments = records,
                TotalRecords = totalRecords,
            };
        }
        public async Task<bool> DeleteEmployeeLeaveApplicationComments(IEnumerable<Guid> commentIds, Guid employeeLeaveApplicationId)
        {
            return await _employeeLeaveApplicationStore.DeleteEmployeeLeaveApplicationComments(commentIds, employeeLeaveApplicationId);
        }
        #endregion Leave Comment
        #region Others
        private async Task<NotificationTemplate> GetNotificationParameters(string employeeCode, EmployeeLeaveApplicationStatusRequestModel model)
        {
            var name = await _employeeStore.GetEmployeeName(employeeCode);
            var employeeId = await _employeeStore.GetEmployeeIdByCode(employeeCode);
            var aprrovedBy = await _employeeStore.GetEmployeeName(model.ApprovedBy);
            var notify = new NotificationTemplate
            {
                TemplateName = "Leave",
                TemplateParameters = new List<NotificationParameters>
                {
                    new NotificationParameters
                    {
                        Name = name,
                        Subject = "Leave request",
                        Status = model.Status.GetEnumDescriptionAttribute(),
                        Dates = $"{model.ApprovedFromDate.Value.ToString("MMMM dd, yyyy")} - {model.ApprovedToDate.Value.ToString("MMMM dd, yyyy")}",
                        TotalDaysHours = $"{model.ApprovedDays} Days",
                        EmployeeId = employeeId,
                        ApprovedBy = aprrovedBy,
                        ApprovedPosition = string.Empty,
                    }
                }
            };

            return notify;
        }
        private async Task SendSlackNotification(string employeeCode, EmployeeLeaveApplicationStatusRequestModel model)
        {
            var flag = await _systemFlagService.GetSystemFlagsByTag("slacksendingpermit");
            if (!string.IsNullOrEmpty(flag?.Value) && flag?.Value == FlagStatus.Enable.GetEnumDescriptionAttribute())
            {
                var slackId = await _employeeStore.GetEmployeeSlackId(employeeCode);
                if (!string.IsNullOrEmpty(slackId))
                {
                    var notify = await GetNotificationParameters(employeeCode, model);
                    notify.TemplateParameters.ForEach(x => x.ChannelId = slackId);
                    await _notificationService.SendNotification(notify, NotificationTypes.Slack);
                }
            }
        }
        private async Task SendEmailNotification(string employeeCode, EmployeeLeaveApplicationStatusRequestModel model)
        {
            var flag = await _systemFlagService.GetSystemFlagsByTag("emailsendingpermit");
            if (!string.IsNullOrEmpty(flag?.Value) && flag?.Value == FlagStatus.Enable.GetEnumDescriptionAttribute())
            {
                var email = await _employeeStore.GetEmployeeEmail(employeeCode);
                if (!string.IsNullOrEmpty(email))
                {
                    var notify = await GetNotificationParameters(employeeCode, model);
                    notify.TemplateParameters.ForEach(x => x.ToMail = email);
                    await _notificationService.SendNotification(notify, NotificationTypes.Email);
                }
            }
        }
        private async Task SendInAppNotification(string employeeCode, EmployeeLeaveApplicationStatusRequestModel model)
        {
            var notify = await GetNotificationParameters(employeeCode, model);
            await _notificationService.SendNotification(notify, NotificationTypes.InApp);
        }
        #endregion Others
    }
}