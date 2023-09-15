using System;
using System.Diagnostics;
using StackExchange.Profiling.OpenSearch;
using StackExchange.Profiling.Internal;
using StackExchange.Profiling.OpenSearch.Internal;

namespace StackExchange.Profiling;

/// <summary>
/// Extension methods for the MiniProfiler.OpenSearch.
/// </summary>
public static class MiniProfilerBaseOptionsExtensions {
    /// <summary>
    /// Adds OpenSearch profiling for MiniProfiler via DiagnosticListener.
    /// </summary>
    /// <typeparam name="T">The specific options type to chain with.</typeparam>
    /// <param name="options">The <see cref="MiniProfilerBaseOptions" /> to register on (just for chaining).</param>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
    public static T AddOpenSearch<T>(this T options) where T : MiniProfilerBaseOptions {
        options.ExcludeOpenSearchAssemblies();

        DiagnosticListener.AllListeners.Subscribe(new OpenSearchDiagnosticListener());

        return options;
    }

    /// <summary>
    /// Excludes OpenSearch assemblies from passed in <paramref name="options"/>, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
    /// </summary>
    /// <param name="options"></param>
    public static void ExcludeOpenSearchAssemblies(this MiniProfilerBaseOptions options) {
        ProfilerUtils.ExcludeOpenSearchAssemblies(options);
    }
}
