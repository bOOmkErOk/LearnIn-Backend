using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Application.Interfaces.Security;
using LearnIn.Application.Interfaces.SMTP;
using Microsoft.EntityFrameworkCore;
using LearnIn.Infrastructure.Data;
using LearnIn.Infrastructure.Security;
using LearnIn.Infrastructure.SMTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearnIn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDataContext, DataContext>();

        var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()!;
        services.AddSingleton(jwtSettings);
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}