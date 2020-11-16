﻿using AppPerformanceMetricsSender.Publishing;
using System;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public sealed class Gen2CollectionCount : NamedPerformanceMetric
    {
        public Gen2CollectionCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => GC.CollectionCount(2);

        public override string Name => "gen2gccount";
    }
}