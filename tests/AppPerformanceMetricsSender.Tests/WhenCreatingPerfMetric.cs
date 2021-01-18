using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.PerformanceMetrics.Memory;
using AppPerformanceMetricsSender.Publishing;
using FluentAssertions;
using FluentAssertions.Primitives;
using System;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenCreatingPerfMetric
    {
        [Fact]
        public void ShouldConformToStatsDFormat()
        {
            var appGroup = "test";
            var metricTags = new[] { new MetricTag("a", "b"), new MetricTag("c", "d") };
            var expectedTagString = "a:b,c:d";
            var stubMetric = new StubMetric(appGroup, metricTags);

            stubMetric.ShouldConformToStatsD(appGroup, expectedTagString);
        }

        [Fact]
        public void ShouldThrowIfAppGroupNullOrEmpty()
        {
            var appGroup = string.Empty;
            var metricTags = new[] { new MetricTag("a", "b"), new MetricTag("c", "d") };
            Assert.Throws<ArgumentException>(() => new Gen0CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen1CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen2CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new ManagedHeapAllocatedMemoryInBytes(appGroup, metricTags));
        }
    }

    internal static class AssertionsExtensions
    {
        internal static AndConstraint<StringAssertions> ShouldConformToStatsD(
            this NamedPerformanceMetric perfMetric,
            string expectedAppGroup,
            string expectedTagString) =>
            perfMetric
                .ToString()
                .Should()
                .Be($"{expectedAppGroup}.{perfMetric.Name}:{perfMetric.Count}|g|1|#{expectedTagString}");
    }

    internal class StubMetric : NamedPerformanceMetric
    {
        public StubMetric(string appGroup, params MetricTag[] tags) :
            base(appGroup, tags)
        {
        }

        public override long Count => 200;

        public override string Name => "stub";
    }
}