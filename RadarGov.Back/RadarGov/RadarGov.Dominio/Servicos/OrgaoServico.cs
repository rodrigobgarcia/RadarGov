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
    public class OrgaoServico
    {
        private readonly IImportacaoTerceiroRepositorio<Orgao> _orgaoRepositorio;
        private readonly Pncp _pncp;

        public OrgaoServico(IImportacaoTerceiroRepositorio<Orgao> orgaoRepositorio)
        {
            _orgaoRepositorio = orgaoRepositorio;
            _pncp = new Pncp();
        }
    }
}
