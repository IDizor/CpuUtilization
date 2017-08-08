using CpuCommon.Extensions;
using CpuData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CpuData
{
    /// <summary>
    /// Implements Unit of Work pattern to work with DB context.
    /// </summary>
    /// <seealso cref="CpuData.Interfaces.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private DbContext dataContext;

        /// <summary>
        /// The disposed flag.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UnitOfWork(IAppDbContext dataContext)
        {
            this.dataContext = dataContext as DbContext;

            if (this.dataContext == null)
            {
                throw new ArgumentException("Microsoft.EntityFrameworkCore.DbContext instance is expected as a dataContext parameter.");
            }
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        public IAppDbContext DataContext {
            get
            {
                return (IAppDbContext)this.dataContext;
            }
        }

        /// <summary>
        /// Saves all changes in data context.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">An exception occurred during updating the database.</exception>
        /// <exception cref="Exception">An exception occurred during updating the database.</exception>
        public async Task<int> Save(string currentUser = "")
        {
            int result = 0;

            try
            {
                var changedEntries = this.dataContext.ChangeTracker.Entries()
                    .Where(entry => entry.State == EntityState.Added ||
                        entry.State == EntityState.Modified ||
                        entry.State == EntityState.Deleted)
                    .ToArray();

                // perform entity tracking and process soft deletable entities
                changedEntries.Each(entry =>
                {
                    switch (entry.State)
                    {
                        case (EntityState.Added):
                            var addedEntity = entry.Entity as ITrackable;

                            if (addedEntity != null)
                            {
                                addedEntity.Created = DateTime.UtcNow;
                                addedEntity.CreatedBy = currentUser;
                                addedEntity.Modified = DateTime.UtcNow;
                                addedEntity.ModifiedBy = currentUser;
                            }

                            break;
                        case (EntityState.Modified):
                            var modifiedEntity = entry.Entity as ITrackable;

                            if (modifiedEntity != null)
                            {
                                modifiedEntity.Modified = DateTime.UtcNow;
                                modifiedEntity.ModifiedBy = currentUser;
                            }

                            break;
                        case (EntityState.Deleted):
                            var deletedEntity = entry.Entity as ISoftDeletable;

                            if (deletedEntity != null)
                            {
                                entry.State = EntityState.Modified;
                                deletedEntity.IsActive = false;

                                var deletedModifiedEntity = entry.Entity as ITrackable;

                                if (deletedModifiedEntity != null)
                                {
                                    deletedModifiedEntity.Modified = DateTime.UtcNow;
                                    deletedModifiedEntity.ModifiedBy = currentUser;
                                }
                            }

                            break;
                    }
                });

                result = await this.dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An exception occurred during updating the database.", ex);
            }

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dataContext.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
