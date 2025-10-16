using RadarGov.Dominio.IReposoitorio;
using RadarGov.Integracoes.Pnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Servicos
{
    public class EsferaServico
    {
        private readonly IEsferaRepositorio _esferaRepositorio;
        private readonly Pncp _pncp;

        public EsferaServico(IEsferaRepositorio esferaRepositorio)
        {
            _esferaRepositorio = esferaRepositorio;
            _pncp = new Pncp();
        }
    }
}
