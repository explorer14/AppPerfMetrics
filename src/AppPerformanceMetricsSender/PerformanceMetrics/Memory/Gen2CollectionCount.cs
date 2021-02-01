using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class Gen2CollectionCount : NamedPerformanceMetric
    {
        public Gen2CollectionCount(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => GC.CollectionCount(2);

        public override string Name => "gen2gccount";
    }
}