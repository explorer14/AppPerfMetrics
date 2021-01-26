using AppPerformanceMetricsSender.PerformanceMetrics;
using StatsdClient;
using System;
using System.Linq;

namespace AppPerformanceMetricsSender.Publishing
{
    internal class DataDogMetricsPublisher : IMetricsPublisher
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

        public void Publish(NamedPerformanceMetric metric)
        {
            if (metric != null)
            {
                dogStatsdService.Gauge(
                        metric.FullyQualifiedName,
                        metric.Value, 1,
                        metric.Tags.Select(x => $"{x.Key}:{x.Value}").ToArray());
            }
        }
    }
}