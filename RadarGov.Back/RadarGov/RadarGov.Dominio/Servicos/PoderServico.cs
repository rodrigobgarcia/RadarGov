using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.Pnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Servicos
{
    public class PoderServico
    {
        private readonly IImportacaoTerceiroRepositorio<Poder> _poderRepositorio;
        private readonly Pncp _pncp;

        public PoderServico(IImportacaoTerceiroRepositorio<Poder> poderRepositorio)
        {
            _poderRepositorio = poderRepositorio;
            _pncp = new Pncp();
        }
    }
}
