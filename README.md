# Introduction

Collects various performance metrics from your .NET Core app and pushes them to DataDog at regular intervals.

# Architecture

[Draw.io diagram](https://viewer.diagrams.net/?highlight=0000ff&nav=1&title=AppPerformanceMetricsSender_Architecture.drawio#R5VtZc6M4EP41rtp5iAshAebRRya7VTM1rspc2TcZZFszGHmFnNjz61cCAQbkYxLb2LupVAW1DtDX%2FXW3jnTgcLF%2B4Hg5%2F8hCEnVsK1x34Khj2wDZdkf9WuEmk%2FRywYzTUDcqBY%2F0F9FCS0tXNCRJpaFgLBJ0WRUGLI5JICoyzDl7qTabsqj61iWekYbgMcBRU%2FqNhmKupcD1y4o%2FCZ3NRT4%2FL6tY4LyxnkkyxyF72RLB%2Bw4ccsZE9rRYD0mkwMtxyfq931FbfBgnsTimw7O3sYK%2FPn16%2FyHZfP2B2eiJ%2FX2nR3nG0UpPWH%2Bs2OQIcLaKQ6IGAR04eJlTQR6XOFC1L1LnUjYXi0hXhziZF21VYYyFIDxOJbalpPqNhAuy3jkVUAAkLYuwBRF8I5voDo6T9dA2BSDKyi%2BlhnxLwz7fUo7jaiHWVjErhi6Bkw8au9%2FA0T4xjicAqVcDyWuCZLsGkFDvXCC5DUxIKMmmi4yLOZuxGEf3pXRQombJUtnmA2NLjdUPIsRGew68EqyKJFlT8X3r%2BUkN1XV0abTWI6eFTV6I5XS%2Fbxe2eqli2S0t5f2y%2BalJ7deZxICteED2YAW1r8N8RsSedshsA5xEWNDn6necXKFew%2BrHq0lEk7lR0R%2FwRAaHinJwRGfKNwQSH8KlQFk8ld63rysWNAwzOyAJ%2FYUn6XgK6iWjsUjn4ww6zmgfZXRo0J1Lh7ytlt32upNfVlf6kwrFci9wNPh67LGaSznwHbC6CBpHzgdh02kizaKuv%2BIj3%2BDIerdH0q4F7CpRETpE1bQ0JpxKwJTlpcIpi4X%2BRtuqR7MWyJ3lEm9gd9q1zznebDXQ1Nlpf24eFbTxIYCqicWB9g7wapaZfcFJ7RSaMhc3kmgOkiWOKxbs%2FrNSSVaq3rsk1W9fJSNguS4r5dNM%2FS08WDaY%2FLhsvKy2DcdWt8rTO7qM9Hs8HfLdmkMCb3N1%2BTCo2wPW9o9zKUcHm7GL8KmUfJQD00A%2BPBLp1HhuCBOe28AfAxz8nKU%2B75HwZxqQd4ZGagFjJUTqJFSfosxAvkvSzpooaEMyxatIvGs9TUSwmidC5DfzRFMyDeC58sQbDEGt5YnoyFDitJkn%2Bg2uPXzpP9z%2FN7LEQ77Thh44qe8s0kTQ3THy%2Bb0namh0hAUesVnhQJWn07FUOdGWvZxX2zKQOuk6DT8HkMHP9ZzdmnkTK5zdGLaMlu9dH1rg9qLCnVyZWKgaGoDrvWZlcsKIAfJt1EMhA7YZMkAzv%2F%2FMBFb9%2BkH6etv6POcEh0nrdAGgShfbam61AdtAlrNtRwL%2F5shi4AoCrXPFvhBXXrVQhzW7g9b%2BhXq9PbJgzdLOsFDPIdxeaHEWkCS5ZgYbNssvy%2BDi1OimGez4btsMPnavrR0GW%2FVE6wCDa%2B2RfYGtNmDYKikYPBx%2FUTyW6rs6EkPUOonRzZE4zVlru%2BmeZ7fN4mO3OdrNWZur4oeh4ojUqLi%2BIIdAc6fvwvywb44fBnr0PL9tejg3QY%2FmhkcZR8acPmNB0u2jBdNvuya2OE7b0QQ0j9sbKEU0VhxJBGc%2FiwtLyl5DyolcNzO1XSrtScl33ts5BXpe7QzRN6BnAO9sZwp20%2Fj6y2VEA5yCYlsDZV%2FYYHgSBFEFK0N3yCKmuByzFPIpjaKa6Ph9a5M5V%2F1cRUFHnwgerzAXVc3dNZi7bTJ3%2B2wacw9oTJ3XMb7AcZC5jXzXOT%2B3%2B1%2FqEfr1PYGGGmHvkmqErS5ky7j%2BtFVzMMYXB3hPlXhuju5mLZ4ksOeXSQ%2FfFPHbjOywucfzjVPRXBJe9rYEOM9tCWiZsc7P5ayu7ec3Ul97xneBi17NbGLI4oRFVWd6vUd4yDMsXy56JGU3z7ULCF28UBDEk2TZMVwbahs8x2%2FGBSN46Pd9hiyWt9ozcy3%2FNwDe%2Fws%3D)

# Usage

This package assumes that you have DataDog agent installed on your host and you have access to your DataDog account to create charts and dashboards. Not much point in collecting metrics if you can't visualise them.

## Quick Setup (TLDR;)

The quick set up uses the following defaults:

- DataDog host: `localhost` and port: `8125`. If you are running on EC2 or Azure VM instances, chances are your DD agent is locally hosted.
- `PerfMetricsSenderOptions` defaults to `10 second` collection interval. This should ideally be the flushing frequency of the DataDog agent for correlated metrics.
- `MetricTags` is empty i.e. no tags are published by default.

Just call the `AddPerfMetricSenderWithDataDog` extension method on `ServiceCollection` instance during bootstrap, and pass in an app identifier:

### Worker Service

```
public class Program
{
    public static void Main(string[] args) => 
        CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();

                services.AddPerfMetricSenderWithDataDog("my worker service"); // <-- Add this line
            });
}
```

### Web API

```
public void ConfigureServices(IServiceCollection services)
{    
    services.AddPerfMetricSenderWithDataDog(appGroup: "my api");
    
    //...other services
}
```

### With Custom Metric Tags

These tags will always be included in the metrics sent to DataDog

```
public void ConfigureServices(IServiceCollection services)
{    
    services.AddPerfMetricSenderWithDataDog(
        appGroup: "my api",        
        tags: new MetricTag("environment", <ASPNETCORE_ENVIRONMENT>));
    
    //...other services
}
```

## Metrics collected

### CPU

- `totalactivethreadcount`: this is the difference between maximum threads that can be spun up by the `ThreadPool` and threads that are available in it. Includes worker and I/O completion threads.
- `processthreadcount`: how many threads your application process has running/spawned.
- `processcputimemillis`: how much time your application process has spent utilising the CPU.

### Memory
- `gen0gccount`: Generation 0 garbage collections (short lived objects)
- `gen1gccount`: Generation 1 garbage collections
- `gen2gccount`: Generation 2 garbage collections (long lived objects)
- `processprivatememorybytes`: Amount of memory that your application process is using itself. Correlates with WorkingSet and PrivateSet.
- `managedheapallocatedmemorybytes`: Approximate number of bytes allocated on the managed heap memory.

## Representation

The metrics published to DataDog use the `StatsD` [format](https://docs.datadoghq.com/developers/dogstatsd/datagram_shell/?tab=metrics).

Its strongly recommended that you assign a unique enough identifier for your app so its metrics can be disambiuguated from other similar metrics from other apps. The sender will automatically replace blank spaces in the identifier with underscores `_` before publishing them to DataDog, this is because DataDog doesn't allow empty spaces in metric names. The format of the StatsD string used by this package looks like this:

`{AppIdentifier}.{MetricName}:{Value}|g|1|{Comma Separated Tags}`

for e.g. a `gen0gccount` for my web api `my api` with tags `tag1` and `tag2` with values `value1` and `value2` respectively will look like this:

`my_api.gen0gccount:5|c|1|#tag1:value1,tag2:value2`

## Custom Metrics

Provide an implementation for the `NamedPerformanceMetric` base class and specify the assembly to load these custom metrics from, for e.g. say `DummyMetric` is defined in the host app with assembly name `WebApplication37`

```
public class DummyMetric : NamedPerformanceMetric
{
    public DummyMetric(string appGroup, params MetricTag[] tags)
        : base(appGroup, tags)
    {
    }

    public override long Count => 777;
    public override string Name => "dummy";
}
```

Then at bootstrap:

```
services.AddPerfMetricSenderWithDataDog(
    appGroup: "my api",    
    assemblyToLoadAdditionalMetricsFrom: Assembly.GetEntryAssembly());
```

Or if the metric is defined in a different assembly for e.g. `Infrastructure`, then:

```
services.AddPerfMetricSenderWithDataDog(
    appGroup: "my api",    
    assemblyToLoadAdditionalMetricsFrom: Assembly.GetAssembly(typeof(DummyMetric))); // <-- this type could be any type that's defined in that assembly, doesn't have to be a metric type
```
