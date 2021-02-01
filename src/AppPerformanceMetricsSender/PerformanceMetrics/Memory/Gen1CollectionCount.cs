using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class Gen1CollectionCount : NamedPerformanceMetric
    {
        public Gen1CollectionCount(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => GC.CollectionCount(1);

        public override string Name => "gen1gccount";
    }
}