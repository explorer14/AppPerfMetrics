﻿using AppPerformanceMetricsSender.Publishing;
using System.Threading;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public sealed class TotalMaxThreadCount : NamedPerformanceMetric
    {
        public TotalMaxThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count
        {
            get
            {
                ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableIoThreads);
                return availableWorkerThreads + availableIoThreads;
            }
        }

        public override string Name => "totalmaxthreads";
    }
}