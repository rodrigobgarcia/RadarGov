using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.Pnc;

namespace RadarGov.Dominio.Servicos
{
    public class EsferaServico
    {
        private readonly IImportacaoTerceiroRepositorio<Esfera> _esferaRepositorio;
        private readonly Pncp _pncp;

        public EsferaServico(IImportacaoTerceiroRepositorio<Esfera> esferaRepositorio)
        {
            _esferaRepositorio = esferaRepositorio;
            _pncp = new Pncp();
        }
    }
}
