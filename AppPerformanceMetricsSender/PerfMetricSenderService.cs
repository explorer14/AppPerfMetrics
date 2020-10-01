﻿using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppPerformanceMetricsSender
{
    public class PerfMetricSenderService : BackgroundService
    {
        private readonly IMetricsPublisher metricPublisher;
        private readonly IReadOnlyCollection<NamedPerformanceMetric> availablePerformanceMetrics;
        private System.Timers.Timer timer;

        public PerfMetricSenderService(
            IMetricsPublisher metricPublisher,
            IReadOnlyCollection<NamedPerformanceMetric> performanceMetrics)
        {
            timer = new System.Timers.Timer(10_000);
            this.metricPublisher = metricPublisher;
            availablePerformanceMetrics = performanceMetrics;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer.Elapsed += (sender, args) =>
            {
                foreach (var metric in availablePerformanceMetrics)
                {
                    metricPublisher.Count(metric);
                }
            };

            timer.Start();

            return Task.CompletedTask;
        }
    }
}