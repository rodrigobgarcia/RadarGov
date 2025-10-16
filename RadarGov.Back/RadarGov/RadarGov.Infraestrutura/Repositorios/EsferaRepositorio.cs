using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.IReposoitorio;
using RadarGov.Infraestrutura.Interfaces;
using RadarGov.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class EsferaRepositorio : IBaseRepositorio<Esfera>, IEsferaRepositorio
    {
        public EsferaRepositorio(RadarGovDbContext context) : base(context)
        {

        }

        public async Task<Esfera> ObterPorIdTerceiroAsync(string idTercerio)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.IdTerceiro == idTercerio);
        }
    }
}
