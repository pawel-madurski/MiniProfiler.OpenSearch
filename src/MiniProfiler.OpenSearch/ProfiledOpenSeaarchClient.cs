using OpenSearch.Client;
using StackExchange.Profiling.OpenSearch.Internal;

namespace StackExchange.Profiling.OpenSearch;

/// <summary>
/// Profiled version of <see cref="OpenSearchClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
/// </summary>
public class ProfiledOpenSearchClient : OpenSearchClient {
    /// <summary>
    /// Provides base <see cref="OpenSearchClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
    /// </summary>
    /// <param name="configuration">Instance of <see cref="ConnectionSettings"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
    public ProfiledOpenSearchClient(ConnectionSettings configuration) : base(configuration) {
        ProfilerUtils.ExcludeOpenSearchAssemblies();
        configuration.OnRequestCompleted(apiCallDetails => MiniProfilerOpenSearch.HandleResponse(apiCallDetails));
    }
}
