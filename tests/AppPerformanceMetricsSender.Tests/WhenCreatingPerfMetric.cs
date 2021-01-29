using AppPerformanceMetricsSender.PerformanceMetrics.Memory;
using AppPerformanceMetricsSender.Publishing;
using System;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenCreatingPerfMetric
    {
        [Fact]
        public void ShouldThrowIfAppPrefixNullOrEmpty()
        {
            var appPrefix = string.Empty;
            var metricTags = new[] { new MetricTag("a", "b"), new MetricTag("c", "d") };
            Assert.Throws<ArgumentException>(() => new Gen0CollectionCount(appPrefix, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen1CollectionCount(appPrefix, metricTags));
            Assert.Throws<ArgumentException>(() => new Gen2CollectionCount(appPrefix, metricTags));
            Assert.Throws<ArgumentException>(() => new ManagedHeapAllocatedMemoryInBytes(appPrefix, metricTags));
        }
    }
}