﻿using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal sealed class Gen1CollectionCount : NamedPerformanceMetric
    {
        public Gen1CollectionCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Value => GC.CollectionCount(1);

        public override string Name => "gen1gccount";
    }
}