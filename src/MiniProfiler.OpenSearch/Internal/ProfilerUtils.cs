using System.Collections.Generic;
using StackExchange.Profiling.Internal;

namespace StackExchange.Profiling.OpenSearch.Internal;

internal static class ProfilerUtils {
    /// <summary>
    /// OpenSearch-related assemblies to exclude from profiling
    /// </summary>
    internal static HashSet<string> ExcludedAssemblies { get; } = new HashSet<string> {
        "OpenSearch.Client",
        "OpenSearch.Net.Diagnostics",
        typeof(MiniProfilerOpenSearch).Namespace,
        typeof(MiniProfilerOpenSearch).Assembly.GetName().Name,
    };

    /// <summary>
    /// Excludes OpenSearch assemblies from passed in <paramref name="options"/>, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
    /// </summary>
    /// <param name="options"></param>
    internal static void ExcludeOpenSearchAssemblies(this MiniProfilerBaseOptions options) {
        foreach (var excludedAssembly in ExcludedAssemblies) {
            options.ExcludeAssembly(excludedAssembly);
        }
    }

    /// <summary>
    /// Excludes OpenSearch assemblies from <see cref="MiniProfiler.DefaultOptions"/>, so they won't be included into <see cref="MiniProfiler"/> timings' call-stack.
    /// </summary>
    internal static void ExcludeOpenSearchAssemblies() => MiniProfiler.DefaultOptions.ExcludeOpenSearchAssemblies();
}
