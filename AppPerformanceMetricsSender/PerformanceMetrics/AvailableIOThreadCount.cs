using AppPerformanceMetricsSender.Publishing;
using System.Threading;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public sealed class AvailableIOThreadCount : NamedPerformanceMetric
    {
        public AvailableIOThreadCount(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Count 
        {
            get
            {
                ThreadPool.GetAvailableThreads(out _, out var iocp);
                return iocp; 
            } 
        }

        public override string Name => "availableiothreads";
    }
}