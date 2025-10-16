using Microsoft.EntityFrameworkCore;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class ImportacaoTerceiroRepositorio : BaseRepositorio<Modalidade>, IImportacaoTerceiroRepositorio
    {
        public ImportacaoTerceiroRepositorio(RadarGovDbContext context) : base(context)
        {
        }

        public async Task<Modalidade?> ObterPorIdTerceiroAsync(string idTerceiro)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.IdTerceiro == idTerceiro);
        }
    }
}
