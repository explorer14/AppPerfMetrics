using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class ManagedHeapAllocatedMemoryInBytes : NamedPerformanceMetric
    {
        public ManagedHeapAllocatedMemoryInBytes(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => GC.GetTotalMemory(false);

        public override string Name => "managedheapallocatedmemorybytes";
    }
}