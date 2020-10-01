using AppPerformanceMetricsSender.Publishing;
using Microsoft.Extensions.DependencyInjection;
using StatsdClient;

namespace AppPerformanceMetricsSender.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPerfMetricSender(
            this IServiceCollection services,
            IMetricsPublisher metricsPublisher = null)
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

            services.AddHostedService<PerfMetricSenderService>();

            return services;
        }
    }
}