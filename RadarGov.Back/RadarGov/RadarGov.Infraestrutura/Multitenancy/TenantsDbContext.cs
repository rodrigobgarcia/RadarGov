using Microsoft.EntityFrameworkCore;

namespace RadarGov.Infraestrutura.Multitenancy
{
    public class TenantsDbContext : DbContext
    {
        public TenantsDbContext(DbContextOptions<TenantsDbContext> options) : base(options) { }
        public DbSet<Tenant> Tenants { get; set; }
    }

}
