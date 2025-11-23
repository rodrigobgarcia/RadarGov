using RadarGov.Dominio.DTOs;
using RadarGov.Integracoes.RadarHub.DTOs;

namespace RadarGov.Dominio.Interfaces
{
    public interface IRadarHubIntegracaoServico
    {
        Task<string> ObterODataLicitacaoAsync(string parametrosOData);
        Task<string> ObterODataModalidadeAsync(string parametrosOData);
        Task<string> ObterODataSegmentoAsync(string parametrosOData);
        Task<string> ObterODataOrgaoAsync(string parametrosOData);
        Task<string> ObterODataMunicipiosAsync(string parametrosOData);
        Task<string> ObterODataUfAsync(string parametrosOData);
        Task<string> ObterODataTipoMargemPreferenciaAsync(string parametrosOData);
        Task<string> ObterODataPoderAsync(string parametrosOData);
        Task<string> ObterODataUnidadeAsync(string parametrosOData);
        Task<string> ObterODataTipoAsync(string parametrosOData);


        Task<ODataResult<ModalidadeDto>> ObterModalidadesAsync(string parametrosOData);
        Task<ODataResult<OrgaoDto>> ObterOrgaosAsync(string parametrosOData);
        Task<ODataResult<MunicipioDto>> ObterMunicipiosAsync(string parametrosOData);
        Task<ODataResult<UfDto>> ObterUfsAsync(string parametrosOData);
        Task<ODataResult<TipoMargemPreferenciaDto>> ObterTipoMargemPreferenciaAsync(string parametrosOData);
        Task<ODataResult<PoderDto>> ObterPoderAsync(string parametrosOData);
        Task<ODataResult<UnidadeDto>> ObterUnidadeAsync(string parametrosOData);
        Task<ODataResult<TipoDto>> ObterTipoAsync(string parametrosOData);
        Task<ODataResult<SegmentoDto>> ObterSegmentoAsync(string parametrosOData);
    }
}
