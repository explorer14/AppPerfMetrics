using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal class ProcessPrivateMemoryInBytes : NamedPerformanceMetric
    {
        private Process appProcess;

        public ProcessPrivateMemoryInBytes(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
            appProcess = Process.GetCurrentProcess();
        }

        public override long Value
        {
            get
            {
                appProcess.Refresh();
                return appProcess.PrivateMemorySize64;
            }
        }

        public override string Name => "processprivatememorybytes";
    }
}