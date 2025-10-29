using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ProjectManagement.Domain.Interfaces;
using ProjectManagement.Infrastructure.Database;
using ProjectManagement.Infrastructure.Repositories;
using ProjectManagement.Infrastructure.Security;
using ProjectManagement.Infrastructure.Settings;
using System.Text;

namespace ProjectManagement.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));


            services.AddSingleton<MongoDbContext>();


            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var context = sp.GetRequiredService<MongoDbContext>();
                return context.Database;
            });
        }


        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //  Bind JwtSettings section to the JwtSettings class
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSettings = configuration.GetSection("JwtSettings");
            var secret = jwtSettings["Key"];

            if (string.IsNullOrEmpty(secret))
            {
                Console.WriteLine(" JWT Secret not found in configuration!");
                throw new InvalidOperationException("JWT Secret missing in configuration.");
            }

            var key = Encoding.UTF8.GetBytes(secret);
            Console.WriteLine(" JWT Secret loaded successfully.");

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

           
            services.AddSingleton<JwtTokenGenerator>();
        }



    }
}
