using CpuData.Interfaces;
using CpuData.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuApi.Services
{
    /// <summary>
    /// Monitores the CPU load and writes data to DB.
    /// </summary>
    public class CpuMonitor
    {
        /// <summary>
        /// The current PC name.
        /// </summary>
        private readonly string pcName;

        /// <summary>
        /// The monitor task.
        /// </summary>
        private readonly Task monitor;

        /// <summary>
        /// The monitoring cancellation token source.
        /// </summary>
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="CpuMonitor" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="cpuStatusRepository">The CPU status repository.</param>
        public CpuMonitor(ILogger<CpuMonitor> logger, IUnitOfWork unitOfWork, ICpuStatusRepository cpuStatusRepository)
        {
            this.pcName = Environment.MachineName;
            this.cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            this.monitor = Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        cpuStatusRepository.Insert(new CpuStatus
                        {
                            PcName = this.pcName,
                            // unable to get CPU load in .NET Core, use random
                            Usage = new Random().NextDouble() * 100,
                            TimeStamp = DateTime.UtcNow
                        }).Wait();

                        unitOfWork.Save().Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                    }

                    Thread.Sleep(2000);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Stops the CPU monitoring.
        /// </summary>
        public void Stop()
        {
            if (this.monitor != null && !this.monitor.IsCompleted
                && !this.monitor.IsCanceled && !this.monitor.IsFaulted)
            {
                this.cancellationTokenSource.Cancel();
            }
        }
    }
}
