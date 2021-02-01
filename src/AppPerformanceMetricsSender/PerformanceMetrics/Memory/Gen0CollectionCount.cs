using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class Gen0CollectionCount : NamedPerformanceMetric
    {        
        public Gen0CollectionCount(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => GC.CollectionCount(0);

        public override string Name => "gen0gccount";
    }
}