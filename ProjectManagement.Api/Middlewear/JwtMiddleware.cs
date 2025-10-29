using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProjectManagement.Api.Middlewear
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                await AttachUserToContext(context, userRepository, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserRepository userRepository, string token)
        {
            try
            {
                var jwtSection = _configuration.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSection["Key"] ?? throw new InvalidOperationException("JWT Key missing"));

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParams, out _);
                var userId = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await userRepository.GetByIdAsync(userId);
                    if (user != null)
                    {
                        context.Items["User"] = user;
                    }
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning("JWT token expired: {Message}", ex.Message);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning("Invalid JWT token: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while validating JWT token");
            }
        }
    }

    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
