using CpuData.Interfaces;
using CpuData.Models;
using Microsoft.EntityFrameworkCore;

namespace CpuData
{
    /// <summary>
    /// The DB context for CPU usage monitoring application.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AppDbContext : DbContext, IAppDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for DB context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DB table for CPU statuses.
        /// </summary>
        public DbSet<CpuStatus> CpuStatuses { get; set; }

        /// <summary>
        /// Gets or sets the DB table for logs.
        /// </summary>
        public DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on application DB context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CpuStatus>().ToTable("CpuStatus");
            modelBuilder.Entity<Log>().ToTable("Log");
        }
    }
}
