using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public class AllocatedMemoryInBytes : NamedPerformanceMetric
    {
        public AllocatedMemoryInBytes(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Count => GC.GetTotalMemory(false);

        public override string Name => $"{AppGroup}.allocatedmemorybytes";
    }
}