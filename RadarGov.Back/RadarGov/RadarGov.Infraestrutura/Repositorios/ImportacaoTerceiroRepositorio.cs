using Microsoft.EntityFrameworkCore;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class ImportacaoTerceiroRepositorio<TEntity> : BaseRepositorio<TEntity>, IImportacaoTerceiroRepositorio<TEntity> where TEntity : BaseImportacaoTerceiro
    {
        public ImportacaoTerceiroRepositorio(RadarGovDbContext context) : base(context)
        {
        }

        public async Task<TEntity?> ObterPorIdTerceiroAsync(string idTerceiro)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.IdTerceiro == idTerceiro);
        }
    }
}
