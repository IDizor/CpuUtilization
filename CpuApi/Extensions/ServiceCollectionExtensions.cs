using CpuData;
using CpuData.Interfaces;
using CpuData.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CpuApi.Extensions
{
    /// <summary>
    /// Extension methods for IServiceCollection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the dependencies for application.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICpuStatusRepository, CpuStatusRepository>();
        }
    }
}
