using System.Diagnostics;
using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;

internal sealed class AllocatedBytesInPagingFile : NamedPerformanceMetric
{
    private Process appProcess;

    public AllocatedBytesInPagingFile(params MetricTag[] tags)
        : base(tags)
    {
        appProcess = Process.GetCurrentProcess();
    }

    public override long Value
    {
        get
        {
            appProcess.Refresh();

            return appProcess.PagedMemorySize64;
        }
    }

    public override string Name => "allocatedbytesinpagingfile";
}