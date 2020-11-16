using AppPerformanceMetricsSender.PerformanceMetrics;

namespace AppPerformanceMetricsSender.Publishing
{
    public interface IMetricsPublisher
    {
        void Count(NamedPerformanceMetric metric);
    }
}