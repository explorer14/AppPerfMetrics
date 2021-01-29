using AppPerformanceMetricsSender.Publishing;
using System.Diagnostics;

namespace AppPerformanceMetricsSender.PerformanceMetrics.Memory
{
    internal class ProcessWorkingSetMemoryInBytes : NamedPerformanceMetric
    {
        private Process appProcess;
        public ProcessWorkingSetMemoryInBytes(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
            appProcess = Process.GetCurrentProcess();
        }

        public override long Value 
        {
            get
            {
                appProcess.Refresh();
                return appProcess.WorkingSet64;
            }
        } 

        public override string Name => "processworkingsetmemorybytes";
    }
}