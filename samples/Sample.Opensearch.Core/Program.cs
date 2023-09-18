using OpenSearch.Client;
using StackExchange.Profiling;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();
services.AddMiniProfiler(options => options.ExcludeOpenSearchAssemblies()).AddOpenSearch();
services.AddSingleton<IOpenSearchClient>(_ => {
    var node = new Uri("http://localhost:9200");
    var connectionSettings = new ConnectionSettings(node).DefaultIndex("opensearch-sample");
    return new OpenSearchClient(connectionSettings);
});

services.Configure<RouteOptions>(options => {
   options.LowercaseUrls = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseMiniProfiler();
app.UseEndpoints(endpoints => {
    endpoints.MapDefaultControllerRoute();
});

app.Run();
