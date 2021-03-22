using Domain;
using Microsoft.EntityFrameworkCore;
using TestMusala.DataAccess;

namespace DataAccess
{
    public class TestDbContext:DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public TestDbContext(DbContextOptions<TestDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new GatewayConfiguration());
        }

    }
}
