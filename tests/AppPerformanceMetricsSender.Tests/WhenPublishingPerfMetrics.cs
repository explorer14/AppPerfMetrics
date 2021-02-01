using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenPublishingPerfMetrics
    {
        [Fact]
        public void ShouldPushThemToMetricsPublisher()
        {
            var fakeMetric1 = new FakeMetric1(new MetricTag("bla", "hello"));
            var fakeMetric2 = new FakeMetric2();
            var stubPublisher = new StubMetricPublisher();
            var publisherService = new PerfMetricPublisherService(
                stubPublisher,
                new NamedPerformanceMetric[] { fakeMetric1, fakeMetric2 });

            publisherService.PublishAll();

            stubPublisher.Metrics.Should().HaveCount(2);
        }
    }

    internal class StubMetricPublisher : IMetricsPublisher
    {
        public List<NamedPerformanceMetric> Metrics { get; internal set; } =
            new List<NamedPerformanceMetric>();

        public void Publish(NamedPerformanceMetric metric)
        {
            Metrics.Add(metric);
        }
    }

    internal class FakeMetric1 : NamedPerformanceMetric
    {
        public FakeMetric1(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => 999;

        public override string Name => "fakester";
    }

    internal class FakeMetric2 : NamedPerformanceMetric
    {
        public FakeMetric2(params MetricTag[] tags)
            : base(tags)
        {
        }

        public override long Value => 888;

        public override string Name => "fakester2";
    }
}