using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.CPU
{
    internal class ProcessCPUTimeInMillis : NamedPerformanceMetric
    {
        private Process appProcess;

        public ProcessCPUTimeInMillis(params MetricTag[] tags)
            : base(tags)
        {
            appProcess = Process.GetCurrentProcess();
        }

        public override long Value
        {
            get
            {
                appProcess.Refresh();
                return (long)appProcess.TotalProcessorTime.TotalMilliseconds;
            }
        }

        public override string Name => "processcputimemillis";
    }
}