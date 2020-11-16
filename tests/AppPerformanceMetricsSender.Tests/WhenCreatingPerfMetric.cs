using AppPerformanceMetricsSender.PerformanceMetrics;
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
            var gen0collection = new Gen0CollectionCount(appGroup, metricTags);
            var gen1collection = new Gen1CollectionCount(appGroup, metricTags);
            var gen2collection = new Gen2CollectionCount(appGroup, metricTags);
            var availableIOThreads = new TotalAvailableThreadCount(appGroup, metricTags);
            var availableWorkerThreads = new TotalAvailableThreadCount(appGroup, metricTags);
            var allocatedMemory = new AllocatedMemoryInBytes(appGroup, metricTags);

            gen0collection.ShouldConformToStatsD(appGroup, expectedTagString);
            gen1collection.ShouldConformToStatsD(appGroup, expectedTagString);
            gen2collection.ShouldConformToStatsD(appGroup, expectedTagString);
            availableIOThreads.ShouldConformToStatsD(appGroup, expectedTagString);
            availableWorkerThreads.ShouldConformToStatsD(appGroup, expectedTagString);
            allocatedMemory.ShouldConformToStatsD(appGroup, expectedTagString);
        }

        [Fact]
        public void ShouldThrowIfAppGroupNullOrEmpty()
        {
            var appGroup = string.Empty;
            var metricTags = new[] { new MetricTag("a", "b"), new MetricTag("c", "d") };
            Assert.Throws<ArgumentException>(() => new Gen0CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen1CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen2CollectionCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new TotalAvailableThreadCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new TotalAvailableThreadCount(appGroup, metricTags));
            Assert.Throws<ArgumentException>(() => new AllocatedMemoryInBytes(appGroup, metricTags));
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
                .Be($"{expectedAppGroup}.{perfMetric.Name}:{perfMetric.Count}|c|1|#{expectedTagString}");
    }
}