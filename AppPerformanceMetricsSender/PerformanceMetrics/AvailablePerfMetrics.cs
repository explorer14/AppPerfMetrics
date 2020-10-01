using AppPerformanceMetricsSender.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public static class AvailablePerfMetrics
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
                .ToList();

            var availableMetrics = new List<NamedPerformanceMetric>();

            foreach (var type in metricTypes)
                availableMetrics.Add(
                    (NamedPerformanceMetric)Activator.CreateInstance(
                        type, appGroup, tags));

            return availableMetrics;
        }
    }
}