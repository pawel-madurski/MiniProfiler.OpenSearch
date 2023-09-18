using System;
using System.Diagnostics;
using System.Threading.Tasks;
using OpenSearch.Client;
using OpenSearch.Net;
using StackExchange.Profiling.OpenSearch;
using Xunit;
using StackExchangeMiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace MiniProfiler.OpenSearch.Tests;

public class OpenSearchClientTests {
    [Fact]
    public async Task OpenSearchCallUnsuccessful_ProfilerTimingErrored() {
        // Arrange
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://non-existing-host.non-existing-tld"));
        var settings = new ConnectionSettings(connectionPool)
            .DefaultIndex("test-index");

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new ProfiledOpenSearchClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexDocumentAsync(person);

        // Assert
        profiler.Root.CustomTimings.TryGetValue("opensearch", out var openSearchTimings);
        Assert.True(openSearchTimings![0].Errored);
    }

    [Fact]
    public async Task DiagnosticListener_IndexDocument_ProfilerIncludesTimings() {
        // Arrange
        using var listener = new OpenSearchDiagnosticListener();
        using var foo = DiagnosticListener.AllListeners.Subscribe(listener);
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
        var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
            .DefaultIndex("test-index");

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new OpenSearchClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexDocumentAsync(person);

        // Assert
        AssertTimings(profiler);
    }

    [Fact]
    public async Task ProfileOpenSearchClient_IndexDocument_ProfilerIncludesTimings() {
        // Arrange
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
        var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
            .DefaultIndex("test-index");

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new ProfiledOpenSearchClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexDocumentAsync(person);

        // Assert
        AssertTimings(profiler);
    }

    [Fact]
    public async Task ProfiledLowLevelClient_IndexDocument_ProfilerIncludesTimings() {
        // Arrange
        var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
        var settings = new ConnectionConfiguration(connectionPool, new InMemoryConnection());

        var profiler = StackExchangeMiniProfiler.StartNew();
        var client = new ProfiledOpenSearchLowLevelClient(settings);
        var person = new { Id = "1" };

        // Act
        await client.IndexAsync<BytesResponse>("test-index", PostData.Serializable(person));

        // Assert
        AssertTimings(profiler);
    }

    private static void AssertTimings(StackExchangeMiniProfiler profiler) {
        var customTimings = profiler.Root.CustomTimings;
        Assert.NotEmpty(customTimings);
        Assert.True(customTimings.TryGetValue("opensearch", out var openSearchTimings));
        Assert.NotEmpty(openSearchTimings);
        Assert.Collection(openSearchTimings, timing => {
            Assert.True(timing.DurationMilliseconds > 0);
        });
    }
}
