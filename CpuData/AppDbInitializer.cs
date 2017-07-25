using CpuData.Interfaces;

namespace CpuData
{
    /// <summary>
    /// Initializes database for application.
    /// </summary>
    public static class AppDbInitializer
    {
        /// <summary>
        /// Initializes the database using specified data context.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public static void Initialize(IAppDbContext dataContext)
        {
            var context = dataContext as AppDbContext;

            context.Database.EnsureCreated();
        }
    }
}
