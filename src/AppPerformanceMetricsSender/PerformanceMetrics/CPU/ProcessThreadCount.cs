using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.CPU
{
    internal class ProcessThreadCount : NamedPerformanceMetric
    {
        private Process appProcess;

        public ProcessThreadCount(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
            appProcess = Process.GetCurrentProcess();
        }

        public override long Value
        {
            get
            {
                appProcess.Refresh();
                return appProcess.Threads.Count;
            }
        }

        public override string Name => "processthreadcount";
    }
}