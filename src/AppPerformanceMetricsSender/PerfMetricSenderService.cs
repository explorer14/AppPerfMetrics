using AppPerformanceMetricsSender.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppPerformanceMetricsSender
{
    internal class PerfMetricSenderService : BackgroundService
    {
        private readonly PerfMetricPublisherService publisherService;
        private System.Timers.Timer timer;

        public PerfMetricSenderService(
            PerfMetricPublisherService publisherService,
            PerfMetricsSenderOptions options)
        {
            timer = new System.Timers.Timer(options.MetricCollectionIntervalInMilliseconds);
            this.publisherService = publisherService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting perf metrics collection...");
            timer.Elapsed += async (sender, args) =>
            {
                await publisherService.PublishAll();
            };

            timer.Start();

            return Task.CompletedTask;
        }
    }
}