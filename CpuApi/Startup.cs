using CpuApi.Extensions;
using CpuApi.Middleware;
using CpuApi.ModelBinders;
using CpuApi.Models;
using CpuApi.Services;
using CpuData;
using CpuData.Interfaces;
using CpuData.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Linq;

namespace CpuApi
{
    public class Startup
    {
        /// <summary>
        /// The configuration for application.
        /// </summary>
        public static IConfigurationRoot Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiConfiguration>(Configuration.GetSection("ApiConfiguration"));
            services.RegisterDependencies();

            services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("AppDb")));

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ArrayModelBinder.Provider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAppDbContext dataContext)
        {
            AppDbInitializer.Initialize(dataContext);

            loggerFactory.AddNLog();
            app.AddNLogWeb();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseMvc();

            var cpuMonitorUnitOfWork = new UnitOfWork(dataContext);
            var cpuMonitor = new CpuMonitor(loggerFactory.CreateLogger<CpuMonitor>(),
                cpuMonitorUnitOfWork, new CpuStatusRepository(cpuMonitorUnitOfWork));
        }
    }
}
