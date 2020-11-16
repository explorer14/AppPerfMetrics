using AppPerformanceMetricsSender.PerformanceMetrics;
using AppPerformanceMetricsSender.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppPerformanceMetricsSender
{
    public static class AvailablePerformanceMetrics
    {
        public static IReadOnlyCollection<NamedPerformanceMetric> All(
            string appGroup,
            Assembly assemblyToLoadAdditionalMetricsFrom = null,
            params MetricTag[] tags)
        {
            var metricTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(NamedPerformanceMetric)))
                .ToList();

            if (assemblyToLoadAdditionalMetricsFrom != null)
                metricTypes.AddRange(assemblyToLoadAdditionalMetricsFrom
                    .GetTypes()
                    .Where(x =>
                        x.IsSubclassOf(typeof(NamedPerformanceMetric)))
                    .ToList());

            var availableMetrics = new List<NamedPerformanceMetric>();

            foreach (var type in metricTypes)
                availableMetrics.Add(
                    (NamedPerformanceMetric)Activator.CreateInstance(
                        type, appGroup, tags));

            return availableMetrics;
        }
    }
}