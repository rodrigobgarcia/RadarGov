using Microsoft.EntityFrameworkCore;
using RadarGov.Dominio.Entidades;

namespace RadarGov.Infraestrutura;

public partial class RadarGovDbContext : DbContext
{
    public DbSet<Modalidade> Modalidades { get; set; }
    public DbSet<Orgao> Orgaos { get; set; }
    public DbSet<Unidade> Unidades { get; set; }
    public DbSet<Ufs> Ufs { get; set; }
    public RadarGovDbContext()
    {
    }

    public RadarGovDbContext(DbContextOptions<RadarGovDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
