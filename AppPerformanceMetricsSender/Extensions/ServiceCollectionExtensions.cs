using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.DependencyInjection;
using StatsdClient;
using System;
using System.Collections.Generic;

namespace AppPerformanceMetricsSender.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPerfMetricSender(
            this IServiceCollection services,
            string appGroup,
            IMetricsPublisher metricsPublisher = null,
            params MetricTag[] tags)
        {
            if (metricsPublisher == null)
                services.AddSingleton<IMetricsPublisher>(svc =>
                    new DataDogMetricsPublisher(
                        new StatsdConfig
                        {
                            StatsdServerName = "localhost",
                            StatsdPort = 8125,
                        }));
            else
                services.AddSingleton(svc => metricsPublisher);

            services.AddTransient(
                svc => AvailablePerfMetrics.All(appGroup, tags));

            services.AddHostedService<PerfMetricSenderService>();

            return services;
        }
    }
}