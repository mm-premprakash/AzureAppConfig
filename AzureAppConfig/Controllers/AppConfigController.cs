using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AzureAppConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppConfigController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public AppConfigController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAllConfigs() {
            var allConfigs = _configuration
                    .AsEnumerable()
                    .Where(c => !string.IsNullOrEmpty(c.Value)) // Exclude null or empty values
                    .ToDictionary(c => c.Key, c => c.Value);
            return Ok(allConfigs);
        }

    }
}
