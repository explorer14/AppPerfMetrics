using AppPerformanceMetricsSender.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppPerformanceMetricsSender
{
    internal class PerfMetricSenderService : IHostedService
    {
        private System.Timers.Timer timer;

        public PerfMetricSenderService(
            PerfMetricPublisherService publisherService,
            PerfMetricsSenderOptions options)
        {
            timer = new System.Timers.Timer(options.MetricCollectionIntervalInMilliseconds);
            timer.Elapsed += (sender, args) =>
                publisherService.PublishAll();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting perf metrics collection...");
            timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Stop();
            return Task.CompletedTask;
        }
    }
}