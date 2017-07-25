using CpuApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CpuApi.Controllers
{
    [Route("api/[controller]")]
    public class CpuController : Controller
    {
        private readonly ILogger<CpuController> _logger;

        public CpuController(ILogger<CpuController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("status")]
        public virtual IActionResult GetStatus()
        {
            return this.JsonOk("CPU Utilization v1.0");
        }
    }
}
