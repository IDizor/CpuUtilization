using CpuData.Interfaces;
using CpuData.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /// The wmic process to get CPU usage percentage.
        /// </summary>
        private readonly Process wmicProcess;

        /// <summary>
        /// The CPU check iterations count.
        /// </summary>
        private const int cpuCheckIterationsCount = 5;

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
            wmicProcess = new Process();
            wmicProcess.StartInfo.UseShellExecute = false;
            wmicProcess.StartInfo.RedirectStandardOutput = true;
            wmicProcess.StartInfo.CreateNoWindow = true;
            wmicProcess.StartInfo.FileName = "wmic.exe";
            wmicProcess.StartInfo.Arguments = "cpu get loadpercentage";

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
                            Usage = GetAverageCpuUsage(),
                            TimeStamp = DateTime.UtcNow
                        }).Wait();

                        unitOfWork.Save().Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                    }

                    Thread.Sleep(300000);
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

        #region Private_Methods
        /// <summary>
        /// Gets the average CPU usage.
        /// </summary>
        /// <param name="iterations">The iterations.</param>
        /// <returns></returns>
        private int GetAverageCpuUsage()
        {
            var cpusUsage = new List<int>();

            for (int i = 0; i < cpuCheckIterationsCount; i++)
            {
                wmicProcess.Start();

                var usages = wmicProcess.StandardOutput.ReadToEnd()
                    .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(line => Int32.Parse(line.Trim()))
                    .ToArray();

                cpusUsage.Add(usages.Sum());
                wmicProcess.WaitForExit();
                Thread.Sleep(1000);
            }

            return (int)Math.Round(cpusUsage.Average());
        }
        #endregion
    }
}
