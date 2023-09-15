using System;
using StackExchange.Profiling.Internal;
using StackExchange.Profiling.OpenSearch;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for the MiniProfiler.OpenSearch.
/// </summary>
public static class MiniProfilerServiceCollectionExtensions {
    /// <summary>
    /// Adds OpenSearch profiling for MiniProfiler via DiagnosticListener.
    /// </summary>
    /// <param name="builder">The <see cref="IMiniProfilerBuilder" /> to add services to.</param>
    /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <c>null</c>.</exception>
    public static IMiniProfilerBuilder AddOpenSearch(this IMiniProfilerBuilder builder) {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));

        builder.Services.AddSingleton<IMiniProfilerDiagnosticListener, OpenSearchDiagnosticListener>();

        return builder;
    }
}
