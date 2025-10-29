using ProjectManagement.Infrastructure.Logging;
using Serilog;

namespace ProjectManagement.Api.Extensions
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddAppLogging(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.Configure<LoggerFilterOptions>(configuration.GetSection("Logging"));

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            return services;
        }
    }
}
