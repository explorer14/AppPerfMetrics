namespace AppPerformanceMetricsSender.Publishing
{
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