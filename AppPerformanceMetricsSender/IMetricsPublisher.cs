using WebApplication37;

namespace AppPerformanceMetricsSender
{
    public interface IMetricsPublisher
    {
        void Count(NamedPerfMetric metric);
    }
}