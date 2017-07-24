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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CpuStatus>().ToTable("CpuStatus");
        }
    }
}
