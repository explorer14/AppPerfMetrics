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
            var availableMetrics = AvailablePerformanceMetrics.All();

            availableMetrics.Should().HaveCount(NUMBER_OF_AVAILABLE_METRICS);
        }
    }
}