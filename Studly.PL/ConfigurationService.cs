using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Studly.BLL.Infrastructure;
using Studly.BLL.Interfaces;
using Studly.BLL.Interfaces.Services;
using Studly.BLL.Services;
using Studly.DAL.EF;
using Studly.Interfaces;
using Studly.Repositories;

namespace Studly.PL;

public class ConfigurationService
{
    public static void ConfigureSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
        services.AddAuthorization();
    }
    public static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        // Uncomment the one you need and comment out the other
        // For SQL Server
        // services.AddDbContext<ApplicationContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // For SQLite
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SQLite")));
    }

    public static void ConfigureAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BLL.Mapper.AutoMapperProfile).Assembly);

    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IChallengeService, ChallengeService>();
    }

    public static void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(o =>
        {
            o.AddDefaultPolicy(p =>
            {
                p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}