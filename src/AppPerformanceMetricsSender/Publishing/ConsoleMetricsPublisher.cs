using AppPerformanceMetricsSender.PerformanceMetrics;

namespace AppPerformanceMetricsSender.Publishing
{
    internal class ConsoleMetricsPublisher : IMetricsPublisher
    {
        public void Publish(NamedPerformanceMetric metric) =>
            System.Console.WriteLine(metric.ToString());
    }
}