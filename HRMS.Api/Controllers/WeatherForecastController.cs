using HRMS.Api.Controllers.Base;
using HRMS.Api.Models;
using HRMS.Core.Consts;
using HRMS.Core.Exstensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]"), Base.AuthorizeAttribute, ApiExplorerSettings(IgnoreApi = true)]
    public class WeatherForecastController : ApiControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestAdmin"), Base.AuthorizeAttribute(RoleTypes.Admin, RoleTypes.Manager)]
        public IEnumerable<WeatherForecast> GetTestAdmin()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestEmployee"), Base.AuthorizeAttribute(RoleTypes.Employee)]
        public IEnumerable<WeatherForecast> GetTestEmployee()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test"), AllowAnonymous]
        public IEnumerable<WeatherForecast> GetTest()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete, AllowAnonymous]
        public IActionResult GetTestDelete()
        {
            return Success(true);
        }
    }
}