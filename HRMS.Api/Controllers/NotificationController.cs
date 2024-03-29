using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Email;
using HRMS.Core.Models.Employee;
using HRMS.Core.Models.Notification;
using HRMS.Core.Models.Slack;
using HRMS.Core.Utilities.Auth;
using HRMS.Resources;
using HRMS.Services.Common;
using HRMS.Services.Common.Builder;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/notification"), Tags("Notification")]
    public class NotificationController : ApiControllerBase
    {
        private readonly INotificationService<NotificationTemplate> _notificationService;
        private readonly IAppResourceAccessor _appResourceAccessor;
        private readonly IEmployeeService _employeeService;
        private readonly IUserContextAccessor _userContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="appResourceAccessor"></param>
        /// <param name="userContextAccessor"></param>
        /// <param name="employeeService"></param>
        public NotificationController(INotificationService<NotificationTemplate> notificationService, IAppResourceAccessor appResourceAccessor, IUserContextAccessor userContextAccessor, IEmployeeService employeeService)
        {
            _notificationService = notificationService;
            _appResourceAccessor = appResourceAccessor;
            _userContextAccessor = userContextAccessor;
            _employeeService = employeeService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Email Notification
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="month"></param>
        /// <param name="emailBody"></param>
        /// <returns></returns>
#if DEBUG
        [HttpGet("email/{employeeCode}")]
#else
        [HttpPost("email/{employeeCode}"), AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager), ApiExplorerSettings(IgnoreApi = true)]
#endif
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> EmailNotification([FromRoute] string employeeCode, [FromRoute] string month, [FromBody] string emailBody)
        {
            var email = await _employeeService.GetEmployeeEmail(employeeCode);
            var name = await _employeeService.GetEmployeeName(employeeCode);
            var notify = new NotificationTemplate
            {
                TemplateText = emailBody,
                TemplateParameters = new List<NotificationParameters>
                {
                    new NotificationParameters
                    {
                        ToMail = email,
                        Subject = "Salary",
                        Name = name
                    }
                }
            };
            var result = await _notificationService.SendNotification(notify, NotificationTypes.Email);
            return string.IsNullOrEmpty(result) ? Warning<string>(_appResourceAccessor.GetResource("General:ErrorUpsertRecords")) : Success(result);
        }
        /// <summary>
        /// Slack Notification
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="template"></param>
        /// <returns></returns>
#if DEBUG
        [HttpGet("slack/{employeeCode}")]
#else
        [HttpPost("slack/{employeeCode}"), AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager), ApiExplorerSettings(IgnoreApi = true)]
#endif
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SlackNotification([FromRoute] string employeeCode, [FromQuery] string template)
        {
            var slackId = ""; // await _employeeService.GetEmployeeSlackId(employeeCode);
            var notify = new NotificationTemplate
            {
                TemplateName = template,
                TemplateParameters = new List<NotificationParameters>
                {
                    new NotificationParameters
                    {
                        ChannelId = slackId
                    }
                }
            };
            var result = await _notificationService.SendNotification(notify, NotificationTypes.Slack);
            return Success();
        }
        /// <summary>
        /// Sync slack information of employee to system
        /// </summary>
        /// <returns></returns>
        [HttpPost("slack/sync"), AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<string>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SyncSlackUserInformation([FromQuery] string employeeEmail = "")
        {
            var result = await _notificationService.SyncSlackUserInformation(employeeEmail);
            return result != null && result.Any() ? Success(result) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable"));
        }
        /// <summary>
        /// Notification for web push
        /// </summary>
        /// <returns></returns>
        [HttpPost("status"), AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SetNotificationStatus([FromBody] IEnumerable<Guid> Ids, [FromQuery] bool readAll = false)
        {
            return Success(await _notificationService.SetNotificationStatus(Ids, readAll));
        }
        /// <summary>
        /// Get All Notifications
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("list"), AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager, RoleTypes.Manager, RoleTypes.Employee)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<NotificationListModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> GetNotifications([FromBody] NotificationFilterModel filter)
        {
            if (_userContextAccessor.UserRole != RoleTypes.SuperAdmin)
            {
                filter.EmployeeId = _userContextAccessor.EmployeeId;
            }
            var result = await _notificationService.GetNotifications(filter);
            return (result != null && result.TotalRecords > 0 ? Success(result, filter.Pagination, result.TotalRecords) : Warning<string>(_appResourceAccessor.GetResource("General:NoRecordsAvailable")));
        }
    }
}
