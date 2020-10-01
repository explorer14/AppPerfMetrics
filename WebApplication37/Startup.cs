using AppPerformanceMetricsSender.Extensions;
using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

namespace WebApplication37
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddPerfMetricSender(
            //    appGroup: "my api",
            //    metricsPublisher: new ConsoleMetricsPublisher(),
            //    assemblyToLoadAdditionalMetricsFrom: Assembly.GetExecutingAssembly(),
            //    tags: new MetricTag("environment", "development"));

            services.AddPerfMetricSender(
                appGroup: "my api",
                tags: new MetricTag("environment", "development"));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class ConsoleMetricsPublisher : IMetricsPublisher
    {
        public void Count(NamedPerformanceMetric metric) =>
            System.Console.WriteLine(metric.ToString());
    }

    public class DummyMetric : NamedPerformanceMetric
    {
        public DummyMetric(string appGroup, params MetricTag[] tags)
            : base(appGroup, tags)
        {
        }

        public override long Count => 777;

        public override string Name => "dummy";
    }
}