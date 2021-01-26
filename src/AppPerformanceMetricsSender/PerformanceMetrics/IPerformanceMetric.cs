namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    internal interface IPerformanceMetric
    {
        long Value { get; }

        string Name { get; }
    }
}