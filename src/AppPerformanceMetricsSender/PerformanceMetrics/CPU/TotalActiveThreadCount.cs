using AppPerformanceMetricsSender.Publishing;
using System.Threading;

namespace AppPerformanceMetricsSender.PerformanceMetrics.CPU
{
    internal class TotalActiveThreadCount : NamedPerformanceMetric
    {
        public TotalActiveThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count
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