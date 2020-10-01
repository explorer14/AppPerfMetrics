using AppPerformanceMetricsSender.Publishing;
using System.Threading;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public class AvailableWorkerThreadCount : NamedPerformanceMetric
    {
        public AvailableWorkerThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count
        {
            get
            {
                ThreadPool.GetAvailableThreads(out var workerThreads, out _);
                return workerThreads;
            }
        }

        public override string Name => "availableworkerthreads";
    }
}