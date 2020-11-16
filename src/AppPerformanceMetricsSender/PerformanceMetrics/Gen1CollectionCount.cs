using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public sealed class Gen1CollectionCount : NamedPerformanceMetric
    {
        public Gen1CollectionCount(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Count => GC.CollectionCount(1);

        public override string Name => "gen1gccount";
    }
}