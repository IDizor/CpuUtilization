using System.Threading.Tasks;

namespace CpuData.Interfaces
{
    /// <summary>
    /// Represents public functionality for base repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Finds the entity by key.
        /// </summary>
        /// <param name="key">The primary key value.</param>
        /// <returns>The entity.</returns>
        Task<T> Find(object key);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task Insert(T entity);
    }
}
