using System.Collections.Generic;

namespace CpuApi.Models
{
    /// <summary>
    /// Represents configuration values for API.
    /// </summary>
    public class ApiConfiguration
    {
        /// <summary>
        /// The paging limits.
        /// </summary>
        public List<int> PagingLimits { get; set; }

        /// <summary>
        /// The default paging limit.
        /// </summary>
        public int DefaultPagingLimit { get; set; }
    }
}
