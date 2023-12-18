using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using LabWebAPI.Services.Mapper;
using LabWebAPI.Contracts.Services;
using LabWebAPI.Services.Services;
using LabWebAPI.Contracts.Helpers;
using Microsoft.Extensions.Configuration;

public static class StartupSetup
{
    public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IProductService, ProductService>();
    }
    public static void ConfigJwtOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config);
    }
}


