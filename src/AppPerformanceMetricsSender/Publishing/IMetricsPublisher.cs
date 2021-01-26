using AppPerformanceMetricsSender.PerformanceMetrics;

namespace AppPerformanceMetricsSender.Publishing
{
    public interface IMetricsPublisher
    {
        void Publish(NamedPerformanceMetric metric);
    }
}