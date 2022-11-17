using DogsWebAPISeg.CustomConfigurationProvider;
using Microsoft.AspNetCore.Mvc;

namespace DogsWebAPISeg.Controllers
{
    [ApiController]
    [Route("confprov")]
    public class CustomConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var metadata = new SecurityMetadata
            {
                ApiKey = _configuration["ApiKey"],
                ApiSecret = _configuration["ApiSecret"]
            };
            return Ok(metadata);
        }
    }
}
