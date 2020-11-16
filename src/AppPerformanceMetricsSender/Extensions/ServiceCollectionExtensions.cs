using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.DependencyInjection;
using StatsdClient;
using System.Reflection;

namespace AppPerformanceMetricsSender.Extensions
{
    public static class ServiceCollectionExtensions
    {
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
                    MetricCollectionInterval = 10_000
                });

            services.AddTransient(
                svc => AvailablePerformanceMetrics.All(
                    appGroup,
                    assemblyToLoadAdditionalMetricsFrom,
                    tags));

            services.AddHostedService<PerfMetricSenderService>();
        }
    }

    public class PerfMetricsSenderOptions
    {
        public int MetricCollectionInterval { get; set; }
    }

    public class DatadogConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}