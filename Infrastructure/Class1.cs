using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;

namespace Infrastructure
{
    public class DummyMetric2 : NamedPerformanceMetric
    {
        public DummyMetric2(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => 777;

        public override string Name => "dummy";
    }
}