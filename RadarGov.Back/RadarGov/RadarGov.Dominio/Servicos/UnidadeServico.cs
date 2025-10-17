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
    public class UnidadeServico
    {
        private readonly IImportacaoTerceiroRepositorio<Unidade> _unidadeRepositorio;
        private readonly Pncp _pncp;

        public UnidadeServico(IImportacaoTerceiroRepositorio<Unidade> unidadeRepositorio)
        {
            _unidadeRepositorio = unidadeRepositorio;
            _pncp = new Pncp();
        }
    }
}
