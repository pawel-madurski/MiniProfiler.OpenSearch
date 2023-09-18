using OpenSearch.Client;
using OpenSearch.Net;
using StackExchange.Profiling.OpenSearch.Internal;

namespace StackExchange.Profiling.OpenSearch;

/// <summary>
/// Profiled version of <see cref="OpenSearchLowLevelClient"/>. Handles responses and pushes data to current <see cref="MiniProfiler"/>'s session.
/// </summary>
public class ProfiledOpenSearchLowLevelClient : OpenSearchLowLevelClient {
    /// <summary>
    /// Provides base <see cref="OpenSearchLowLevelClient"/> with profiling features to current <see cref="MiniProfiler"/> session.
    /// </summary>
    /// <param name="configuration">Instance of <see cref="ConnectionConfiguration"/>. Its responses will be handled and pushed to <see cref="MiniProfiler"/></param>
    public ProfiledOpenSearchLowLevelClient(ConnectionConfiguration configuration) : base(configuration) {
        ProfilerUtils.ExcludeOpenSearchAssemblies();
        configuration.OnRequestCompleted(apiCallDetails => MiniProfilerOpenSearch.HandleResponse(apiCallDetails));
    }
}
