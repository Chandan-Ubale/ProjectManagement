using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Interface;
using ProjectManagement.Application.Services;
using FluentValidation;
using System.Reflection;

namespace ProjectManagement.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();

            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
