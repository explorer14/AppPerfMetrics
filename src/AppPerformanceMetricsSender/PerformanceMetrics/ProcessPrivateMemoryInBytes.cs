using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    internal class ProcessPrivateMemoryInBytes : NamedPerformanceMetric
    {
        public ProcessPrivateMemoryInBytes(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => Process.GetCurrentProcess().PrivateMemorySize64;

        public override string Name => "processprivatememorybytes";
    }
}