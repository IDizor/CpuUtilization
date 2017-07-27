using CpuApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CpuApi.Controllers
{
    [Route("api/[controller]")]
    public class CpuController : Controller
    {
        public CpuController()
        {
        }

        [HttpGet]
        [Route("status")]
        public virtual IActionResult GetStatus()
        {
            return this.JsonOk("CPU Utilization v1.0");
        }
    }
}
