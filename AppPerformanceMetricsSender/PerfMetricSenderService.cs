using AppPerformanceMetricsSender;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication37
{
    public class PerfMetricSenderService : BackgroundService
    {
        private readonly IMetricsPublisher metricPublisher;
        private System.Timers.Timer timer;

        public PerfMetricSenderService(IMetricsPublisher metricPublisher)
        {
            timer = new System.Timers.Timer(10_000);
            this.metricPublisher = metricPublisher;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer.Elapsed += (sender, args) =>
            {
                foreach (var metric in AvailablePerfMetrics.All())
                {
                    metricPublisher.Count(metric);
                }
            };

            timer.Start();

            return Task.CompletedTask;
        }
    }

    public interface IPerfMetric
    {
        long Count { get; }

        string Name { get; }
    }

    public abstract class NamedPerfMetric : IPerfMetric
    {
        private readonly string tags;

        protected NamedPerfMetric(string appGroup, params MetricTag[] tags)
        {
            AppGroup = appGroup.ToLower().Replace(" ", "_");
            this.tags = tags != null ? 
                string.Join(",", tags.Select(x => $"{x.Key}:{x.Value}")) : 
                string.Empty;
            Tags = tags;
        }

        public abstract long Count { get; }
        public abstract string Name { get; }
        public MetricTag[] Tags { get; }
        protected string AppGroup { get; }

        public override string ToString()
        {
            var statsDString = $"{Name}:{Count}|c|1";

            if (!string.IsNullOrWhiteSpace(tags))
                statsDString += $"|#{tags}";

            return statsDString;
        }
    }

    public class CurrentThreadPoolThreadCount : NamedPerfMetric
    {
        public CurrentThreadPoolThreadCount(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Count 
        {
            get
            {
                ThreadPool.GetAvailableThreads(out _, out var iocp);
                return iocp; 
            } 
        }

        public override string Name => $"{AppGroup}.availableiothreads";
    }

    public class AllocatedMemoryInBytes : NamedPerfMetric
    {
        public AllocatedMemoryInBytes(string appGroup, params MetricTag[] tags) 
            : base(appGroup, tags)
        {
        }

        public override long Count => GC.GetTotalMemory(false);

        public override string Name => $"{AppGroup}.allocatedmemorybytes";
    }

    public static class AvailablePerfMetrics
    {
        public static IReadOnlyCollection<NamedPerfMetric> All() =>
            new NamedPerfMetric[]
            {
                new AllocatedMemoryInBytes("my api",
                    new MetricTag("environment", "development")),
                new CurrentThreadPoolThreadCount("my api", 
                    new MetricTag("environment", "development"))
            };
    }

    public readonly struct MetricTag
    {
        public MetricTag(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}