using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal class ProcessWorkingSetMemoryInBytes : NamedPerformanceMetric
    {
        public ProcessWorkingSetMemoryInBytes(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Value => Process.GetCurrentProcess().WorkingSet64;

        public override string Name => "processworkingsetmemorybytes";
    }
}