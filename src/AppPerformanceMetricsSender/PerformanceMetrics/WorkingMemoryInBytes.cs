using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public class WorkingMemoryInBytes : NamedPerformanceMetric
    {
        public WorkingMemoryInBytes(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => Process.GetCurrentProcess().WorkingSet64;

        public override string Name => "workingmem";
    }
}