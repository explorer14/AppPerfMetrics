using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class ManagedHeapAllocatedMemoryInBytes : NamedPerformanceMetric
    {
        public ManagedHeapAllocatedMemoryInBytes(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Value => GC.GetTotalMemory(false);

        public override string Name => "managedheapallocatedmemorybytes";
    }
}