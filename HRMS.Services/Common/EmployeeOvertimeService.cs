using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Leave;
using HRMS.Core.Models.Notification;
using HRMS.Core.Models.Overtime;
using HRMS.Core.Utilities.General;
using HRMS.DBL.Extensions;
using HRMS.DBL.Stores;
using HRMS.Services.Common.Builder;
using HRMS.Services.Interfaces;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services.Common
{
    public class EmployeeOvertimeService : IEmployeeOvertimeService
    {
        private readonly EmployeeOvertimeStore _employeeOvertimeStore;
        private readonly EmployeeStore _employeeStore;
        private readonly NotificationStore _notificationStore;

        private readonly IEmployeeContractService _employeeContractService;
        private readonly IGeneralUtilities _generalUtilities;
        private readonly INotificationService<NotificationTemplate> _notificationService;
        private readonly ISystemFlagService _systemFlagService;

        public EmployeeOvertimeService(EmployeeOvertimeStore employeeOvertimeStore, IGeneralUtilities generalUtilities, EmployeeStore employeeStore, IEmployeeContractService employeeContractService,
            INotificationService<NotificationTemplate> notificationService, ISystemFlagService systemFlagService, NotificationStore notificationStore)
        {
            _employeeOvertimeStore = employeeOvertimeStore;
            _generalUtilities = generalUtilities;
            _employeeStore = employeeStore;
            _employeeContractService = employeeContractService;
            _notificationService = notificationService;
            _systemFlagService = systemFlagService;
            _notificationStore = notificationStore;
        }

        public async Task<int> AddorUpdateEmployeeOvertime(EmployeeOvertimeRequest request)
        {
            var result = await _employeeOvertimeStore.AddorUpdateEmployeeOvertime(request);
            if (result > 0)
            {
                var message = _notificationService.GetTemplateText("Overtime", NotificationTypes.InApp);
                var employee = await _employeeStore.GetEmployeeDetails(request.EmployeeCode);
                message = message.Replace("%Employee%", $"{employee?.Employee.FirstName} {employee?.Employee.LastName} ({employee?.Employee.EmployeeCode})").Replace("%TOTALDAYSHOURS%", $"{request.OverTimeMinutes} Hours").Replace("%DATES%", request.OverTimeDate.Value.ToLocalTime().ToString("MMMM dd, yyyy"));

                var higerUps = await _employeeStore.GetEmployeeInformationByDesignation(new List<int?> { (int)DesignationTypes.HRExecutive, (int)DesignationTypes.Director }, true, null, null);
                var managers = higerUps?.Select(x => x.EmployeeId).ToList();
                managers.AddRange(request.ProjectManagerIds);

                var notifications = new List<NotificationModel>();
                foreach (var item in managers?.GroupBy(x => x).Select(x => x.FirstOrDefault()).ToList())
                {
                    notifications.Add(new NotificationModel
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = item,
                        HasRead = false,
                        Description = message,
                        Title = "Overtime Request",
                        Type = NotificationTypes.InApp.GetEnumDescriptionAttribute()
                    });
                }
                await _notificationStore.AddNotifications(notifications);
            }
            return result;
        }
        public async Task<EmployeeOvertimeListModel> GetEmployeeOvertime(string employeeCode, RoleTypes? roleType, EmployeeOvertimeFilterModel filter)
        {
            var data = await _employeeOvertimeStore.GetEmployeeOvertime(employeeCode, roleType, filter);
            var result = data?.ToEmployeeOvertimes(false).ToList();

            if (roleType == RoleTypes.Manager)
            {
                var assignedTo = await _employeeOvertimeStore.GetEmployeeAssignedOvertime(employeeCode);
                var assignedResult = assignedTo?.ToEmployeeOvertimes(true).ToList();
                result.AddRange(assignedResult);
            }

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = result?.Count() ?? 0;
                result = result?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeOvertimeListModel
            {
                EmployeeOvertimeRecords = result,
                TotalRecords = totalRecords
            };
        }
        public async Task<EmployeeOvertimeModel> GetEmployeeOvertimeById(int id, string employeeCode, RoleTypes? userRole)
        {
            var data = await _employeeOvertimeStore.GetEmployeeOvertimeById(id, employeeCode, userRole);
            return data?.ToEmployeeOvertimes().FirstOrDefault();
        }
        public async Task<bool?> SetOvertimeStatus(EmployeeOverTimeStatusModel model)
        {
            var employeeId = await _employeeStore.GetEmployeeIdByCode(model.EmployeeCode);
            var contractId = await _employeeContractService.GetEmployeeCurrentContractIdByEmployeeId(employeeId);
            if (contractId == null)
            {
                return null;
            }

            if (model.StatusType == EmployeeOverTimeStatusType.Approved && model.ApprovedMinutes != null && model.OverTimeMinutes != null)
            {
                if (model.OverTimeMinutes >= model.ApprovedMinutes)
                {
                    var fixGrossData = await _employeeContractService.GetEmployeeFixGrossDetails((int)contractId);
                    DateTime overTimeDate = (DateTime)model.OverTimeDate;
                    var overtimeAmount = _generalUtilities.GetOverTimeAmountCalculation(fixGrossData.CostToCompany, DateTime.DaysInMonth(overTimeDate.Year, overTimeDate.Month), (model.ApprovedMinutes?.TotalMinutes ?? 0d));
                    model.OverTimeAmount = overtimeAmount;
                }
            }

            if (model.ApprovedBy <= 0)
            {
                return null;
            }

            var result = await _employeeOvertimeStore.SetOvertimeStatus(model);
            await SendSlackNotification(model.EmployeeCode, model);
            await SendEmailNotification(model.EmployeeCode, model);
            await SendInAppNotification(model.EmployeeCode, model);
            return result;
        }
        public async Task<EmployeeOvertimeListModel> GetAllEmployeeOvertimes(EmployeeOvertimeFilterModel filter, RoleTypes? userRole, int? employeeId)
        {
            var data = await _employeeOvertimeStore.GetAllEmployeeOvertimes(filter, userRole, employeeId);
            var records = data?.ToEmployeeOvertimes().ToList();

            var totalRecords = 0;
            if (filter.Pagination != null && filter.Pagination.PageNumber > 0 && filter.Pagination.PageSize > 0)
            {
                totalRecords = records?.Count() ?? 0;
                records = records?.Skip((filter.Pagination.PageNumber - 1) * filter.Pagination.PageSize).Take(filter.Pagination.PageSize).ToList();
            }

            return new EmployeeOvertimeListModel
            {
                EmployeeOvertimeRecords = records,
                TotalRecords = totalRecords
            };
        }
        public async Task<bool> DeleteEmployeeOvertime(int id, string employeeCode)
        {
            return await _employeeOvertimeStore.DeleteEmployeeOvertime(id, employeeCode);
        }
        public async Task<bool> RemoveEmployeeOvertime(int id)
        {
            return await _employeeOvertimeStore.RemoveEmployeeOvertime(id);
        }
        #region Others
        private async Task<NotificationTemplate> GetNotificationParameters(string employeeCode, EmployeeOverTimeStatusModel model)
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
                        Subject = "Overtime request",
                        Status = model.StatusType.GetEnumDescriptionAttribute(),
                        Dates = model.OverTimeDate.Value.ToLocalTime().ToString("MMMM dd, yyyy"),
                        TotalDaysHours = $"{model.ApprovedMinutes} Hours",
                        EmployeeId = employeeId,
                        ApprovedBy = aprrovedBy,
                        ApprovedPosition = string.Empty,
                    }
                }
            };

            return notify;
        }
        private async Task SendSlackNotification(string employeeCode, EmployeeOverTimeStatusModel model)
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
        private async Task SendEmailNotification(string employeeCode, EmployeeOverTimeStatusModel model)
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
        private async Task SendInAppNotification(string employeeCode, EmployeeOverTimeStatusModel model)
        {
            var notify = await GetNotificationParameters(employeeCode, model);
            await _notificationService.SendNotification(notify, NotificationTypes.InApp);
        }
        #endregion Others
    }
}
