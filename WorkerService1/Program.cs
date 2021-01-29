using AppPerformanceMetricsSender.Extensions;
using AppPerformanceMetricsSender.PerformanceMetrics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args) => 
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddPerformanceMetricSender("my worker service",
                        options: new PerfMetricsSenderOptions
                        {
                            MetricCollectionIntervalInMilliseconds = 2000
                        });
                });
    }
}