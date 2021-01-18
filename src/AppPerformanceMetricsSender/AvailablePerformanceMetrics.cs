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
            Assembly assemblyToLoadAdditionalMetricsFrom = null,
            params MetricTag[] tags)
        {
            var metricTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(NamedPerformanceMetric)))
                .ToDictionary(x=>x.FullName, y=>y);

            if (assemblyToLoadAdditionalMetricsFrom != null)
            {
                var customMetricTypes = assemblyToLoadAdditionalMetricsFrom
                    .GetTypes()
                    .Where(x =>
                        x.IsSubclassOf(typeof(NamedPerformanceMetric)))
                    .ToList();

                foreach (var type in customMetricTypes)
                    if (!metricTypes.ContainsKey(type.FullName))
                        metricTypes.Add(type.FullName, type);
            }
                

            var availableMetrics = new List<NamedPerformanceMetric>();

            foreach (var type in metricTypes.Values)
                availableMetrics.Add(
                    (NamedPerformanceMetric)Activator.CreateInstance(
                        type, appGroup, tags));

            return availableMetrics;
        }
    }
}