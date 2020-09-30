using AppPerformanceMetricsSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StatsdClient;

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
            services.AddSingleton<IMetricsPublisher>(svc =>
                new DataDogMetricsPublisher(
                    new StatsdConfig
                    {
                        StatsdServerName = "localhost",
                        StatsdPort = 8125,
                    }));

            //services.AddSingleton<IMetricsPublisher>(svc =>
            //    new ConsoleMetricsPublisher());

            services.AddHostedService<PerfMetricSenderService>();
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
        public void Count(NamedPerfMetric metric) => 
            System.Console.WriteLine(metric.ToString());
    }
}