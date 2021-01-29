using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.DependencyInjection;
using StatsdClient;

namespace AppPerformanceMetricsSender.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add performance metrics sender that publishes to DataDog
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appPrefix">Identifier string for the application</param>
        /// <param name="datadogConfig"></param>
        /// <param name="options"></param>
        /// <param name="tags">Custom tags that will always be publised to DataDog</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddPerformanceMetricSender(
            this IServiceCollection services,
            string appPrefix,
            StatsdConfig datadogConfig = null,
            PerfMetricsSenderOptions options = null,
            params MetricTag[] tags)
        {
            if (datadogConfig == null)
                datadogConfig = new StatsdConfig
                {
                    StatsdServerName = "localhost",
                    StatsdPort = 8125
                };

            services.AddSingleton<IMetricsPublisher>(svc =>
                    new DataDogMetricsPublisher(datadogConfig));

            AddMetricsAndHostedService(
                services,
                appPrefix,
                options,
                tags);

            return services;
        }

        private static void AddMetricsAndHostedService(
            IServiceCollection services,
            string appGroup,
            PerfMetricsSenderOptions options = null,
            params MetricTag[] tags)
        {
            services.AddSingleton(options ??
                new PerfMetricsSenderOptions
                {
                    MetricCollectionIntervalInMilliseconds = 60_000
                });

            services.AddTransient(
                svc => AvailablePerformanceMetrics.All(
                    appGroup,
                    tags));

            services.AddSingleton<PerfMetricPublisherService>();

            services.AddHostedService<PerfMetricSenderService>();
        }
    }

    public class PerfMetricsSenderOptions
    {
        /// <summary>
        /// Interval in milliseconds to collect and publish metrics at
        /// </summary>
        public uint MetricCollectionIntervalInMilliseconds { get; set; }
    }
}