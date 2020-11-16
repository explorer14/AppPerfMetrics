using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public sealed class Gen0CollectionCount : NamedPerformanceMetric
    {
        public Gen0CollectionCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => GC.CollectionCount(0);

        public override string Name => "gen0gccount";
    }
}