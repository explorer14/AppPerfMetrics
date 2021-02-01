using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class HeapSizeInBytes : NamedPerformanceMetric
    {
        public HeapSizeInBytes(params MetricTag[] tags) 
            : base(tags)
        {
        }

        public override long Value => GC.GetGCMemoryInfo().HeapSizeBytes;

        public override string Name => "heapsizebytes";
    }
}