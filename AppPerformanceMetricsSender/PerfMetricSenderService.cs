using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication37
{
    public class PerfMetricSenderService : BackgroundService
    {
        private readonly IMetricsPublisher metricPublisher;
        private System.Timers.Timer timer;

        public PerfMetricSenderService(IMetricsPublisher metricPublisher)
        {
            timer = new System.Timers.Timer(10_000);
            this.metricPublisher = metricPublisher;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer.Elapsed += (sender, args) =>
            {
                foreach (var metric in AvailablePerfMetrics.All())
                {
                    metricPublisher.Count(metric);
                }
            };

            timer.Start();

            return Task.CompletedTask;
        }
    }
}