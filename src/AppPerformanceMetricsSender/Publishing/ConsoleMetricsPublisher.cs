using AppPerformanceMetricsSender.PerformanceMetrics;

namespace AppPerformanceMetricsSender.Publishing
{
    public class ConsoleMetricsPublisher : IMetricsPublisher
    {
        public void Count(NamedPerformanceMetric metric) =>
            System.Console.WriteLine(metric.ToString());
    }
}