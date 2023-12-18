using LabWebApi.Web.Middlewares;
using LabWebAPI.Contracts.Data;
using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.Helpers;
using LabWebAPI.Database.Data.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LabWebApi.Web.Extensions
{
    public static class StartupSetup
    {
       
        public static async void AddSystemRolesToDb(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogDebug("create roles");
                    System.Diagnostics.Debug.WriteLine("create roles");
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await SeedingRoles.SystemRoles(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the roles into DB.");
                }
            }
        }
        public static async void AddProductToDb(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogDebug("create product for admin");
                    System.Diagnostics.Debug.WriteLine("create product for admin");
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var productRepository = services.GetRequiredService<IRepository<Product>>();
                    await SeedingProduct.BasicProduct(userManager, productRepository);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the product into DB.");
                }
            }
        }
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            System.Diagnostics.Debug.WriteLine("This is a log");
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then " +
                "your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
});


                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoWorking.WebApi", Version = "v1" });
            });
        }


    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            };
            cfg.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        context.Response.Headers.Add("Access-Control-Expose-Headers", "Token-Expired");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }
    }
}