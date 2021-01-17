using AppPerformanceMetricsSender.PerformanceMetrics;

namespace AppPerformanceMetricsSender.Publishing
{
    internal class ConsoleMetricsPublisher : IMetricsPublisher
    {
        public void Count(NamedPerformanceMetric metric) =>
            System.Console.WriteLine(metric.ToString());
    }
}