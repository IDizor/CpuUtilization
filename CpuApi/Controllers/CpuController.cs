using CpuApi.Extensions;
using CpuApi.Models;
using CpuApi.Validation;
using CpuData.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CpuApi.Controllers
{
    [Route("api/[controller]")]
    public class CpuController : Controller
    {
        /// <summary>
        /// The API configuration.
        /// </summary>
        private readonly ApiConfiguration configuration;

        /// <summary>
        /// The CPU statuses repository.
        /// </summary>
        private readonly ICpuStatusRepository cpuStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CpuController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cpuStatusRepository">The cpu status repository.</param>
        public CpuController(IOptions<ApiConfiguration> configuration,
            ICpuStatusRepository cpuStatusRepository)
        {
            this.configuration = configuration.Value;
            this.cpuStatusRepository = cpuStatusRepository;
        }

        /// <summary>
        /// Gets the API status.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("status")]
        public virtual IActionResult GetStatus()
        {
            return this.JsonOk("CPU Utilization v0.1");
        }

        /// <summary>
        /// Gets the CPU utilization history.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>Array of CPU statuses.</returns>
        [HttpGet]
        [Route("utilization")]
        public virtual async Task<IActionResult> GetCpuUtilization([FromQuery] int? offset, [FromQuery] int? limit)
        {
            Validator.Requires(!offset.HasValue || offset.Value >= 0, ErrorCode.OutOfRange, nameof(offset));
            Validator.Requires(!limit.HasValue || this.configuration.PagingLimits.Contains(limit.Value), ErrorCode.Invalid, nameof(limit));

            if (!limit.HasValue)
            {
                limit = this.configuration.DefaultPagingLimit;
            }

            var records = await this.cpuStatusRepository.GetCpuStatuses(offset.HasValue ? offset.Value : 0, limit.Value);
            var totalCount = await this.cpuStatusRepository.GetCpuStatusesCount();

            return this.JsonOk(records, totalCount);
        }
    }
}
