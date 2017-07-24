using CpuData.Interfaces;
using CpuData.Models;

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
    }
}
