using System.Diagnostics;
using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;

internal class VirtualMemoryAllocatedInBytes : NamedPerformanceMetric
{
    private Process appProcess;
    public VirtualMemoryAllocatedInBytes(string appGroup, params MetricTag[] tags)
        : base(appGroup, tags)
    {
        appProcess = Process.GetCurrentProcess();
    }
    
    public override long Value
    {
        get
        {
            appProcess.Refresh();
            return appProcess.VirtualMemorySize64;
        }
    }
    public override string Name => "virtualmemorybytesallocated";
}