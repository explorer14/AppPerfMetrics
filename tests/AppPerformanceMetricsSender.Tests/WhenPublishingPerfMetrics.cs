using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.PerformanceMetrics.Memory;
using AppPerformanceMetricsSender.Publishing;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenPublishingPerfMetrics
    {
        [Fact]
        public async Task ShouldPushThemToMetricsPublisher()
        {
            var gen0CountMetric = new Gen0CollectionCount("test");
            var fakeMetric = new FakeMetric("test", new MetricTag("bla", "hello"));
            var stubPublisher = new StubMetricPublisher();
            var publisherService = new PerfMetricPublisherService(
                stubPublisher,
                new NamedPerformanceMetric[] { gen0CountMetric, fakeMetric });

            await publisherService.PublishAll();

            stubPublisher.Metrics.Should().HaveCount(2);
        }
    }

    internal class StubMetricPublisher : IMetricsPublisher
    {
        public List<NamedPerformanceMetric> Metrics { get; internal set; } =
            new List<NamedPerformanceMetric>();

        public void Count(NamedPerformanceMetric metric)
        {
            Metrics.Add(metric);
        }
    }

    internal class FakeMetric : NamedPerformanceMetric
    {
        public FakeMetric(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => 999;

        public override string Name => "fakester";
    }
}