using AppPerformanceMetricsSender.Publishing;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    /// <summary>
    /// Base class for performance metrics. Inherit from this class to define
    /// custom metrics for your app.
    /// </summary>
    public abstract class NamedPerformanceMetric : IPerformanceMetric
    {
        /// <summary>
        /// Instantiate the base class
        /// </summary>
        /// <param name="tags">Custom tags i.e. name value pairs to associate with this metric</param>
        protected NamedPerformanceMetric(params MetricTag[] tags) => Tags = tags;

        /// <summary>
        /// The value of this metric. For time based metrics prefer [milliseconds] to give yourself
        /// more fine grained value, for space based metrics prefer [bytes]. Any conversion to coarser
        /// grained units like MB, seconds, minutes etc should be done on the monitoring platform
        /// </summary>
        public abstract long Value { get; }

        /// <summary>
        /// Used by the publisher to fully identify the metric
        /// </summary>
        public string FullyQualifiedName => $"perf.{Name}";

        /// <summary>
        /// Name for the metric. Will be lower cased and spaces removed
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Tags to associate with the metric for e.g. environment,
        /// country etc.
        /// </summary>
        public MetricTag[] Tags { get; }
    }
}