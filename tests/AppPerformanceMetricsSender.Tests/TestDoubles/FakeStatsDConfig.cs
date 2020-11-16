using StatsdClient;

namespace AppPerformanceMetricsSender.Tests.TestDoubles
{
    internal static class FakeStatsDConfig
    {
        internal static StatsdConfig Default => new StatsdConfig
        {
            StatsdServerName = "test",
            StatsdPort = 2000
        };
    }
}