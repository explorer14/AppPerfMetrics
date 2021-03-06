﻿using AppPerformanceMetricsSender.Publishing;
using System.Threading;

namespace AppPerformanceMetricsSender.PerformanceMetrics.CPU
{
    internal class TotalActiveThreadCount : NamedPerformanceMetric
    {
        public TotalActiveThreadCount(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value
        {
            get
            {
                ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxIoThreads);
                ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableIoThreads);
                return maxWorkerThreads - availableWorkerThreads + (maxIoThreads - availableIoThreads);
            }
        }

        public override string Name => "totalactivethreadcount";
    }
}