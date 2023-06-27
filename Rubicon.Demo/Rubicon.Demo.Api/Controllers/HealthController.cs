using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rubicon.Demo.Api.Services.Interfaces;

namespace Rubicon.Demo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;

        public HealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }

        /// <summary>
        /// Health check
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var isHealthy = await _healthService.IsHealthy();

            if (!isHealthy)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
