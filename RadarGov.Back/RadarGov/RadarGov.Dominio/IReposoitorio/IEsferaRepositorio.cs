using RadarGov.Dominio.Entidades;
using RadarGov.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.IReposoitorio
{
    public interface IEsferaRepositorio : IBaseRepositorio<Esfera>
    {
        Task<Esfera> ObterPorIdTerceiroAsync(string idTerceiro);
    }
}
