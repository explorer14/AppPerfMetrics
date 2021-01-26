using AppPerformanceMetricsSender.Publishing;
using System;
using System.Linq;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    /// <summary>
    /// Base class for performance metrics. Inherit from this class to define
    /// custom metrics for your app.
    /// </summary>
    public abstract class NamedPerformanceMetric : IPerformanceMetric
    {
        private readonly string tags;

        /// <summary>
        /// Instantiate the base class
        /// </summary>
        /// <param name="appGroup">Identifier for the app</param>
        /// <param name="tags">Custom tags i.e. name value pairs to associate with this metric</param>
        protected NamedPerformanceMetric(string appGroup, params MetricTag[] tags)
        {
            if (string.IsNullOrWhiteSpace(appGroup))
                throw new ArgumentException("App group cannot be null or empty", nameof(appGroup));

            AppGroup = appGroup.ToLower().Replace(" ", "_");
            this.tags = tags != null ?
                string.Join(",", tags.Select(x => $"{x.Key}:{x.Value}")) :
                string.Empty;
            Tags = tags;
        }

        /// <summary>
        /// The value of this metric. For time based metrics prefer [milliseconds] to give yourself
        /// more fine grained value, for space based metrics prefer [bytes]. Any conversion to coarser
        /// grained units like MB, seconds, minutes etc should be done on the monitoring platform
        /// </summary>
        public abstract long Value { get; }

        /// <summary>
        /// Used by the publisher to fully identify the metric
        /// </summary>
        public string FullyQualifiedName => $"{AppGroup}.{Name}";

        /// <summary>
        /// Name for the metric. Will be lower cased and spaces removed
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Tags to associate with the metric for e.g. environment, 
        /// country etc.
        /// </summary>
        public MetricTag[] Tags { get; }

        /// <summary>
        /// App identifier string for e.g. "finance-api"
        /// </summary>
        protected string AppGroup { get; }

        public override string ToString()
        {
            var statsDString = $"{FullyQualifiedName}:{Value}|g|1";

            if (!string.IsNullOrWhiteSpace(tags))
                statsDString += $"|#{tags}";

            return statsDString;
        }
    }
}