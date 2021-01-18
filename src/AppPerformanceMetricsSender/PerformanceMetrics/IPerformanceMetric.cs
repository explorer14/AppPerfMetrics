namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    internal interface IPerformanceMetric
    {
        long Count { get; }

        string Name { get; }
    }
}