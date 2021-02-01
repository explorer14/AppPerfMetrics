using AppPerformanceMetricsSender.PerformanceMetrics;
using StatsdClient;
using System;
using System.Linq;

namespace AppPerformanceMetricsSender.Publishing
{
    internal class DataDogMetricsPublisher : IMetricsPublisher
    {
        private readonly IDogStatsd dogStatsdService;

        public DataDogMetricsPublisher(StatsdConfig config)
            : this(new DogStatsdService(), config)
        {
        }

        public DataDogMetricsPublisher(IDogStatsd dogStatsdService, StatsdConfig config)
        {
            this.dogStatsdService = dogStatsdService ??
                throw new ArgumentNullException(nameof(dogStatsdService));

            dogStatsdService.Configure(config ??
                throw new ArgumentNullException(nameof(config)));
        }

        public void Publish(NamedPerformanceMetric metric)
        {
            if (metric != null)
            {
                dogStatsdService.Gauge(
                        metric.FullyQualifiedName,
                        metric.Value,
                        sampleRate: 1,
                        metric.Tags.Select(x => $"{x.Key}:{x.Value}").ToArray());
            }
        }
    }
}