using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.CPU
{
    internal class ProcessThreadCount : NamedPerformanceMetric
    {
        public ProcessThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Value => Process.GetCurrentProcess().Threads.Count;

        public override string Name => "processthreadcount";
    }
}