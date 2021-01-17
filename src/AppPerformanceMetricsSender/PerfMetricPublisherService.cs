﻿using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppPerformanceMetricsSender
{
    public class PerfMetricPublisherService
    {
        private readonly IMetricsPublisher metricsPublisher;
        private readonly IReadOnlyCollection<NamedPerformanceMetric> availablePerfMetrics;

        public PerfMetricPublisherService(
            IMetricsPublisher metricsPublisher,
            IReadOnlyCollection<NamedPerformanceMetric> availablePerfMetrics)
        {
            this.metricsPublisher = metricsPublisher;
            this.availablePerfMetrics = availablePerfMetrics;
        }

        public Task PublishAll()
        {
            foreach (var metric in availablePerfMetrics)
            {
                metricsPublisher.Count(metric);
            }

            return Task.CompletedTask;
        }
    }
}