using AppPerformanceMetricsSender.Publishing;
using AppPerformanceMetricsSender.Tests.TestDoubles;
using Moq;
using StatsdClient;
using System;
using Xunit;

namespace AppPerformanceMetricsSender.Tests
{
    public class WhenInvokingDataDogMetricsPublisher
    {
        [Fact]
        public void ShouldThrowIfConfigNull()
        {
            Assert.Throws<ArgumentException>(() => new DataDogMetricsPublisher(null));
        }

        [Fact]
        public void ShouldThrowIfDogstatsDServiceIsNull()
        {
            Assert.Throws<ArgumentException>(() => new DataDogMetricsPublisher(
                null,
                FakeStatsDConfig.Default));
        }

        [Fact]
        public void ShouldNotPublishMetricIfMetricIsNull()
        {
            var mockDog = new Mock<IDogStatsd>();
            var publisher = new DataDogMetricsPublisher(
                mockDog.Object,
                FakeStatsDConfig.Default);

            publisher.Publish(null);
            mockDog.Verify(x =>
                x.Counter(
                    It.IsAny<string>(),
                    It.IsAny<long>(),
                    It.IsAny<double>(),
                    It.IsAny<string[]>()),
                Times.Never);
        }
    }
}