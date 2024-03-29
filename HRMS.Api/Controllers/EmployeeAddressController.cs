using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Models.Employee;
using HRMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/employee/address"), Tags("Address"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.HRManager)]
    public class EmployeeAddressController : ApiControllerBase
    {
        private readonly IEmployeeAddressService _employeeAddressService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeAddressService"></param>
        public EmployeeAddressController(IEmployeeAddressService employeeAddressService)
        {
            _employeeAddressService = employeeAddressService;
        }
        /// <summary>
        /// Add employee address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddEmployeeAddress([FromBody] EmployeeAddressModel model)
        {
            var result = await _employeeAddressService.AddAddress(model);
            return Success(result);
        }
        /// <summary>
        /// Update employee address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> UpdateEmployeeAddresses([FromBody] EmployeeAddressModel model)
        {
            var result = await _employeeAddressService.UpdateAddress(model);
            return Success(result);
        }
        /// <summary>
        /// Get employee address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<EmployeeAddressModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> GetEmployeeAddressByCode([FromRoute] int id)
        {
            var result = await _employeeAddressService.GetAddressById(id);
            return Success(result);
        }
        /// <summary>
        /// Delete employee address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeleteEmployeeAddress([FromRoute] int id)
        {
            var result = await  _employeeAddressService.DeleteAddress(id);
            return Success(result);
        }
    }
}