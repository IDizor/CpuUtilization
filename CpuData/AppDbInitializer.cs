using CpuData.Interfaces;
using CpuData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

            if (context.CpuStatuses.Any())
            {
                return;
            }

            context.CpuStatuses.Add(new CpuStatus
            {
                TimeStamp = DateTime.UtcNow,
                PcName = "PcName",
                Usage = 0
            });

            context.SaveChanges();
        }
    }
}
