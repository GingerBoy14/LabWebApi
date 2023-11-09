using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Database.Data.Configurations;
using Microsoft.EntityFrameworkCore;
namespace LabWebAPI.Database.Data
{
    public class LabWebApiDbContext : DbContext
    {
        public LabWebApiDbContext(DbContextOptions<LabWebApiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public DbSet<User> Users { get; set; }
    }
}