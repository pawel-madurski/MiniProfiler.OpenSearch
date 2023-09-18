# MiniProfiler.OpenSearch

*** This project has been forked from https://github.com/romansp/MiniProfiler.Elasticsearch and compiled against OpenSearch ***

Put your [OpenSearch.Net and OpencSearch.Client](https://github.com/opensearch-project/opensearch-net) requests timings directly into [MiniProfiler](https://github.com/MiniProfiler/dotnet).

[![Build status](https://ci.appveyor.com/api/projects/status/e9axfh54cvn3qqti/branch/main?svg=true)](https://ci.appveyor.com/project/pawel-madurski/miniprofiler-opensearch/branch/main)
![Nuget](https://img.shields.io/nuget/v/DexxLab.MiniProfiler.OpenSearch)

![image](https://github.com/pawel-madurski/MiniProfiler.OpenSearch/assets/11866857/74293aab-6ad3-4ee1-8014-908d831b9646)

## Usage
You have two options on how to start profiling your OpenSearch requests.

### Option 1. Register in services collection
In your `Startup.cs`, call `AddOpenSearch()`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMiniProfiler(options => {
        options.ExcludeOpenSearchAssemblies();
    })
    .AddOpenSearch();
}
```

### Option 2. Create profiled client manually
Update usages of `OpenSearchClient` or `OpenSearchLowLevelClient` with their respected profiled version `ProfiledOpenSearchClient` or `ProfiledOpenSearchLowLevelClient`.

```c#
services.AddSingleton<IOpenSearchClient>(x => 
{
    var node = new Uri("http://localhost:9200");
    var connectionSettings = new ConnectionSettings(node).DefaultIndex("opensearch-sample");
    return new ProfiledOpenSearchClient(connectionSettings);
});
```

## Sample
See [Sample.OpenSearch.Core](https://github.com/pawel-madurski/MiniProfiler.OpenSearch/tree/main/samples/Sample.OpenSearch.Core) for working example.
