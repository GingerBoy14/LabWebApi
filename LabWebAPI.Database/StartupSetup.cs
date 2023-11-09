using LabWebAPI.Contracts.Data;
using LabWebAPI.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class StartupSetup
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    }
    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<LabWebApiDbContext>(x => x.UseSqlServer(connectionString));
    }
}