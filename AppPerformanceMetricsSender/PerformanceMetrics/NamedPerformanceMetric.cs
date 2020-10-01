using AppPerformanceMetricsSender.Publishing;
using System.Linq;

namespace AppPerformanceMetricsSender.PerformanceMetrics
{
    public abstract class NamedPerformanceMetric : IPerformanceMetric
    {
        private readonly string tags;

        protected NamedPerformanceMetric(string appGroup, params MetricTag[] tags)
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
}