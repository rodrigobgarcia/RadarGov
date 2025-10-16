using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RadarGov.Infraestrutura;
using System.IO;

namespace RadarGov.Repositorio
{
    public class RadarGovDbContextFactory : IDesignTimeDbContextFactory<RadarGovDbContext>
    {
        public RadarGovDbContext CreateDbContext(string[] args)
        {
            var connectionString = "server=localhost;user id=root;password=mysql;database=db_radargov;";

            var optionsBuilder = new DbContextOptionsBuilder<RadarGovDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new RadarGovDbContext(optionsBuilder.Options);
        }
    }
}
