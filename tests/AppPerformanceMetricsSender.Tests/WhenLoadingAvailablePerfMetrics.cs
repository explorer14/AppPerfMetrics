using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using FluentAssertions;
using System.Reflection;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenLoadingAvailablePerfMetrics
    {
        [Fact]
        public void ShouldAlwaysLoadAllDefaultMetrics()
        {
            var availableMetrics = AvailablePerformanceMetrics.All("test");

            availableMetrics.Should().HaveCount(8);
        }

        [Fact]
        public void ShouldAppendAssemblySpecificMetricsToAvailableMetrics()
        {
            var availableMetrics = AvailablePerformanceMetrics.All(
                "test", Assembly.GetAssembly(typeof(WhenLoadingAvailablePerfMetrics)));

            availableMetrics.Should().HaveCountGreaterThan(8);
        }
    }

    internal class DummyMetric : NamedPerformanceMetric
    {
        public DummyMetric(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => 1;

        public override string Name => "dummy";
    }
}