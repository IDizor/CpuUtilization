using CpuData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CpuData.Repositories
{
    /// <summary>
    /// Implementation for common functionality for repositories.
    /// </summary>
    /// <typeparam name="T">The repository entity type.</typeparam>
    /// <seealso cref="CpuData.Interfaces.IRepositoryBase{T}" />
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>
        protected DbContext DataContext { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.DataContext = unitOfWork.DataContext as DbContext;
        }

        /// <summary>
        /// Finds the entity by key.
        /// </summary>
        /// <param name="key">The primary key value.</param>
        /// <returns>The entity.</returns>
        public async Task<T> Find(object key)
        {
            return await this.DataContext.Set<T>().FindAsync(key);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task Insert(T entity)
        {
            await this.DataContext.Set<T>().AddAsync(entity);
        }
    }
}
