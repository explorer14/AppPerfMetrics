namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public interface IPerformanceMetric
    {
        long Count { get; }

        string Name { get; }
    }
}