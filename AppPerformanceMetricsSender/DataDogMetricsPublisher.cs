using StatsdClient;
using System.Linq;
using WebApplication37;

namespace AppPerformanceMetricsSender
{
    public class DataDogMetricsPublisher : IMetricsPublisher
    {
        private readonly StatsdConfig config;

        public DataDogMetricsPublisher(StatsdConfig config)
        {
            this.config = config;
        }

        public void Count(NamedPerfMetric metric)
        {
            using (var dd = new DogStatsdService())
            {
                dd.Configure(config);
                dd.Counter(metric.Name, metric.Count, 1, metric.Tags.Select(x => $"{x.Key}:{x.Value}").ToArray());
            }
        }
    }
}