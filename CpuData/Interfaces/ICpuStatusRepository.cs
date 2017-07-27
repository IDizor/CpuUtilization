using CpuData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CpuData.Interfaces
{
    /// <summary>
    /// Represents functionality for CPU status repository.
    /// </summary>
    public interface ICpuStatusRepository : IRepositoryBase<CpuStatus>
    {
        /// <summary>
        /// Gets the CPU statuses.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        Task<List<CpuStatus>> GetCpuStatuses(int offset, int limit);

        /// <summary>
        /// Gets the CPU statuses total count.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCpuStatusesCount();
    }
}
