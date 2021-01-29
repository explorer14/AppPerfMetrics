using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using System.Collections.Generic;

namespace AppPerformanceMetricsSender
{
    internal class PerfMetricPublisherService
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

        public void PublishAll()
        {
            foreach (var metric in availablePerfMetrics)
                metricsPublisher.Publish(metric);
        }
    }
}