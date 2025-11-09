using RSK.Dominio.Entidades;
using RSK.Dominio.IRepositorios;
using RSK.Infraestrutura.Repositorios;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class RepositorioBaseRadarGov<TEntity>
    : RepositorioBaseAssincrono<TEntity, RadarGovDbContext>, IRepositorioBaseAssincrono<TEntity>
    where TEntity : EntidadeBase
    {
        public RepositorioBaseRadarGov(RadarGovDbContext context) : base(context) { }
    }

}
