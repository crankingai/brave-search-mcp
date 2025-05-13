using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
   // Do not interfere with MCP - send all logs to go to stderr
   consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
await builder.Build().RunAsync();

[McpServerToolType]
public static class WebSearchTool
{
   [McpServerTool]
   [Description("Searches the web. Similar to Google or Bing search.")]
   public static async Task<string> SearchWeb(string query)
   {
      // await Task.Delay(500);
      // return "answer hello";
      return await BraveSearchService.SearchWeb(query);
   }
}
