using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.DependencyInjection;
using StatsdClient;
using System.Reflection;

namespace AppPerformanceMetricsSender.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add performance metrics sender that publishes to DataDog
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appGroup">Identifier string for the application</param>
        /// <param name="datadogConfig"></param>
        /// <param name="options"></param>
        /// <param name="assemblyToLoadAdditionalMetricsFrom">
        /// Load additional metrics from your custom assembly</param>
        /// <param name="tags">Custom tags that will always be publised to DataDog</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddPerfMetricSenderWithDataDog(
            this IServiceCollection services,
            string appGroup,
            DatadogConfig datadogConfig = null,
            PerfMetricsSenderOptions options = null,
            Assembly assemblyToLoadAdditionalMetricsFrom = null,
            params MetricTag[] tags)
        {
            if (datadogConfig == null)
                datadogConfig = new DatadogConfig
                {
                    Host = "localhost",
                    Port = 8125
                };

            services.AddSingleton<IMetricsPublisher>(svc =>
                    new DataDogMetricsPublisher(
                        new StatsdConfig
                        {
                            StatsdServerName = datadogConfig.Host,
                            StatsdPort = datadogConfig.Port,
                        }));

            AddMetricsAndHostedService(
                services,
                appGroup,
                options,
                assemblyToLoadAdditionalMetricsFrom,
                tags);

            return services;
        }

        /// <summary>
        /// Add performance metrics sender that publishes to console in DogStatsD format
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appGroup">Identifier string for the application</param>
        /// <param name="options"></param>
        /// <param name="assemblyToLoadAdditionalMetricsFrom">
        /// Load additional metrics from your custom assembly</param>
        /// <param name="tags">Custom tags that will always be publised to DataDog</param>
        /// <returns></returns>
        public static IServiceCollection AddPerfMetricSender(
            this IServiceCollection services,
            string appGroup,
            PerfMetricsSenderOptions options = null,
            IMetricsPublisher metricsPublisher = null,
            Assembly assemblyToLoadAdditionalMetricsFrom = null,
            params MetricTag[] tags)
        {
            services.AddSingleton(svc => metricsPublisher ??
                new ConsoleMetricsPublisher());

            AddMetricsAndHostedService(
                services,
                appGroup,
                options,
                assemblyToLoadAdditionalMetricsFrom,
                tags);

            return services;
        }

        private static void AddMetricsAndHostedService(
            IServiceCollection services,
            string appGroup,
            PerfMetricsSenderOptions options = null,
            Assembly assemblyToLoadAdditionalMetricsFrom = null,
            params MetricTag[] tags)
        {
            services.AddSingleton(options ??
                new PerfMetricsSenderOptions
                {
                    MetricCollectionIntervalInMilliseconds = 10_000
                });

            services.AddTransient(
                svc => AvailablePerformanceMetrics.All(
                    appGroup,
                    assemblyToLoadAdditionalMetricsFrom,
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

    public class DatadogConfig
    {
        /// <summary>
        /// DataDog server
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// DataDog port (default is 8125)
        /// </summary>
        public int Port { get; set; }
    }
}