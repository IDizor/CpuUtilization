using CpuData.Interfaces;
using CpuData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpuData.Repositories
{
    /// <summary>
    /// Implements functionality to manage CPU statuses.
    /// </summary>
    /// <seealso cref="CpuData.Repositories.RepositoryBase{CpuData.Models.CpuStatus}" />
    /// <seealso cref="CpuData.Interfaces.ICpuStatusRepository" />
    public class CpuStatusRepository : RepositoryBase<CpuStatus>, ICpuStatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CpuStatusRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CpuStatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Gets the CPU statuses.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public async Task<List<CpuStatus>> GetCpuStatuses(int offset, int limit)
        {
            var query = this.Records()
                .OrderByDescending(status => status.TimeStamp)
                .Skip(offset)
                .Take(limit);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the CPU statuses total count.
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCpuStatusesCount()
        {
            return await this.Records().CountAsync();
        }
    }
}
