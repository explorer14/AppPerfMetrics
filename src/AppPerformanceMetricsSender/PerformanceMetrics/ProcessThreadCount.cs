using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public class ProcessThreadCount : NamedPerformanceMetric
    {
        public ProcessThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => Process.GetCurrentProcess().Threads.Count;

        public override string Name => "processthreadcount";
    }
}