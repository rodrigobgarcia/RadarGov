using Microsoft.EntityFrameworkCore;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Repositorio;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class ModalidadeRepositorio : BaseRepositorio<Modalidade>, IModalidadeRepositorio
    {
        public ModalidadeRepositorio(RadarGovDbContext context) : base(context)
        {
        }

        public async Task<Modalidade?> ObterPorIdTerceiroAsync(string idTerceiro)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.IdTerceiro == idTerceiro);
        }
    }
}
