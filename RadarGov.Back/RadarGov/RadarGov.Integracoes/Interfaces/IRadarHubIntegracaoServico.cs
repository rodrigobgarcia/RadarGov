using System.Collections.Generic;
using System.Threading.Tasks;
using RadarGov.Dominio.DTOs;
using RadarGov.Integracoes.Pncp;

namespace RadarGov.Dominio.Interfaces
{
    public interface IRadarHubIntegracaoServico
    {
        Task<IEnumerable<ModalidadeDto>> ObterModalidades();
        Task<IEnumerable<OrgaoDto>> ObterOrgaos();
        Task<IEnumerable<MunicipioDto>> ObterMunicipios(string uf = null);
        Task<IEnumerable<UfDto>> ObterUfs();

        /// <summary>
        /// Busca licitações (raw) no RadarHub. Retorna o DTO de integração (LicitacaoResponse).
        /// </summary>
        Task<IEnumerable<LicitacaoResponse>> ObterLicitacoesRecomendadas(long empresaId);
    }
}