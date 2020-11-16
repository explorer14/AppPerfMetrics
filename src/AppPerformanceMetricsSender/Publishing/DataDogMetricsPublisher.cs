using AppPerformanceMetricsSender.PerformanceMetrics;
using StatsdClient;
using System;
using System.Linq;

namespace AppPerformanceMetricsSender.Publishing
{
    public class DataDogMetricsPublisher : IMetricsPublisher
    {
        private readonly StatsdConfig config;
        private readonly IDogStatsd dogStatsdService;

        public DataDogMetricsPublisher(StatsdConfig config)
            : this(new DogStatsdService(), config)
        {
        }

        public DataDogMetricsPublisher(IDogStatsd dogStatsdService, StatsdConfig config)
        {
            this.config = config ??
                throw new ArgumentException(
                    "DataDog configuration cannot be null", nameof(config));

            this.dogStatsdService = dogStatsdService ??
                throw new ArgumentException(
                    "DataDog service cannot be null", nameof(dogStatsdService));

            this.dogStatsdService = dogStatsdService;
            dogStatsdService.Configure(config);
        }

        public void Count(NamedPerformanceMetric metric)
        {
            if (metric != null)
            {
                dogStatsdService.Counter(
                        metric.FullyQualifiedName,
                        metric.Count, 1,
                        metric.Tags.Select(x => $"{x.Key}:{x.Value}").ToArray());
            }
        }
    }
}