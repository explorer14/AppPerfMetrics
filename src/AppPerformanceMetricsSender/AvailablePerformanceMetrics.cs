using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppPerformanceMetricsSender
{
    internal static class AvailablePerformanceMetrics
    {
        public static IReadOnlyCollection<NamedPerformanceMetric> All(
            string appGroup,
            params MetricTag[] tags)
        {
            var metricTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(NamedPerformanceMetric)))
                .ToDictionary(x=>x.FullName, y=>y);

            var availableMetrics = new List<NamedPerformanceMetric>();

            return metricTypes.Values.Select(x => 
                    Activator.CreateInstance(x, appGroup, tags))
                .Cast<NamedPerformanceMetric>()
                .ToList();
        }
    }
}