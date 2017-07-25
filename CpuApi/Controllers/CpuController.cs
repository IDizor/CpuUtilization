using CpuApi.Extensions;
using CpuApi.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CpuApi.Controllers
{
    [Route("api/[controller]")]
    public class CpuController : Controller
    {
        [HttpGet]
        [Route("status")]
        public virtual async Task<IActionResult> GetStatus()
        {
            Validator.Requires(false, ErrorCode.Invalid, "name");

            return this.JsonOk(await Task.FromResult(DateTime.Now));
        }
    }
}
