using System;
using System.Threading.Tasks;

namespace CpuData.Interfaces
{
    /// <summary>
    /// Represents common scope of functionality to work with DB context.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the data context.
        /// </summary>  
        IAppDbContext DataContext { get; }

        /// <summary>
        /// Saves all changes in data context.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        Task<int> Save(string currentUser = "");
    }
}
