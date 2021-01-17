using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    internal class ProcessCPUTimeInMillis : NamedPerformanceMetric
    {
        public ProcessCPUTimeInMillis(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count =>
            (long)Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds;

        public override string Name => "processcputimemillis";
    }
}