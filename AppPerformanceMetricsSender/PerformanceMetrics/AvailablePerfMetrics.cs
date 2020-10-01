using AppPerformanceMetricsSender.Publishing;
using System.Collections.Generic;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public static class AvailablePerfMetrics
    {
        public static IReadOnlyCollection<NamedPerformanceMetric> All() =>
            new NamedPerformanceMetric[]
            {
                new AllocatedMemoryInBytes("my api",
                    new MetricTag("environment", "development")),
                new CurrentThreadPoolThreadCount("my api", 
                    new MetricTag("environment", "development"))
            };
    }
}