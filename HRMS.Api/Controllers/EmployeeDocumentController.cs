using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Document;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/document"), Tags("Document"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeDocumentController : ApiControllerBase
    {
        private readonly IEmployeeDocumentService _employeeDocumentService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeDocumentService"></param>
        public EmployeeDocumentController(IEmployeeDocumentService employeeDocumentService)
        {
            _employeeDocumentService = employeeDocumentService;
        }
        /// <summary>
        /// Add or Update employee document
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ManageDocument([FromForm] ManageDocumentModel model)
        {
            using MemoryStream stream = new MemoryStream();
            if (model.FormFile != null && model.FormFile.Length > 0)
            {
                await model.FormFile.CopyToAsync(stream, CancellationToken.None);
            }
            var documentModel = new EmployeeDocumentModel
            {
                ContentType = model.FormFile?.ContentType ?? "",
                FileStream = stream?.ToArray() ?? null,
                EmployeeId = model.EmployeeId,
                DocumentTypeId = model.DocumenTypeId,
                FileName = model.FormFile?.FileName ?? "",
                FolderPath = model.FolderPath?.ToLower(),
                IsPrivateFile = model.PrivateFile,
            };
            var result = await _employeeDocumentService.ManageDocument(documentModel);
            return Success(result);
        }
        /// <summary>
        /// Delete employee document
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteDocument([FromBody] DeleteDocumentModel model)
        {
            var result = await _employeeDocumentService.DeleteDocument(model);
            return Success(result);
        }
        /// <summary>
        /// Get employee document by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeDocumentModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeDocumentById([FromRoute] int employeeId)
        {
            var result = await _employeeDocumentService.GetEmployeeDocumentById(employeeId);
            return Success(result);
        }
        /// <summary>
        /// Get Employee Contracts by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}/contracts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<EmployeeContractDocumentModel>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeContracts([FromRoute] int employeeId)
        {
            var result = await _employeeDocumentService.GetEmployeeContracts(employeeId);
            return result != null && result.Any() ? Success(result) : Warning<string>("General:NoRecordsAvailable");
        }
    }
}
