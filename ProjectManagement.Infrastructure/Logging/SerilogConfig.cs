using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectManagement.Infrastructure.Logging
{
    public static class SerilogConfig
    {
        public static void ConfigureLogging(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) 
                .Enrich.FromLogContext()
                .WriteTo.Console()                     
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)    
                .CreateLogger();

            hostBuilder.UseSerilog();
        }
    }
}
