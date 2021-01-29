using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using FluentAssertions;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenLoadingAvailablePerfMetrics
    {
        private const int NUMBER_OF_AVAILABLE_METRICS = 10;

        [Fact]
        public void ShouldAlwaysLoadAllDefaultMetrics()
        {
            var availableMetrics = AvailablePerformanceMetrics.All("test");

            availableMetrics.Should().HaveCount(NUMBER_OF_AVAILABLE_METRICS);
        }
    }

    internal class DummyMetric : NamedPerformanceMetric
    {
        public DummyMetric(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Value => 1;

        public override string Name => "dummy";
    }
}