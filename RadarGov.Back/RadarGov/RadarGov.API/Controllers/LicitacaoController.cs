using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using RadarGov.Integracoes.Pncp;

namespace RadarGov.API.Controllers
{
    [Route("api/[controller]")]
    public class LicitacaoController : ControllerBase
    {
        private readonly LicitacaoRecomendacaoServico _recomendacaoServico;
        private readonly IRadarHubIntegracaoServico _radarHubIntegracaoServico;
        private readonly EmpresaServico _empresaServico;

        public LicitacaoController(LicitacaoRecomendacaoServico recomendacaoServico, IRadarHubIntegracaoServico radarHubIntegracaoServico, EmpresaServico empresaServico)
        {
            _recomendacaoServico = recomendacaoServico;
            _radarHubIntegracaoServico = radarHubIntegracaoServico;
            _empresaServico = empresaServico;
        }

        [HttpGet("recomendacoes")]
        public async Task<IActionResult> GetRecomendacoes([FromQuery] long empresaId, [FromQuery] int maxResultados = 50)
        {
            // 1) Busca todas as licitações no RadarHub (raw DTO)
            var raw = await _radarHubIntegracaoServico.ObterLicitacoesRecomendadas(empresaId);

            // 2) Mapeia para entidade de domínio
            var licitacoes = raw.Select(i => new Licitacao
            {
                Titulo = i.Title,
                Descricao = i.Description,
                ItemUrl = i.ItemUrl,
                ValorGlobal = i.ValorGlobal,
                TemResultado = i.TemResultado,
                DataPublicacaoPncp = i.DataPublicacaoPncp,
                OrgaoIdTerceiro = i.OrgaoId,
                MunicipioIdTerceiro = i.MunicipioId,
                ModalidadeIdTerceiro = i.ModalidadeLicitacaoId,
                TipoIdTerceiro = i.TipoId,
                UfIdTerceiro = i.Uf,
                FonteOrcamentariaIdTerceiro = i.FonteOrcamentariaId,
                TipoMargemPreferenciaIdTerceiro = i.TipoMargemPreferenciaId,
                UnidadeIdTerceiro = i.UnidadeId,
                PoderIdTerceiro = i.PoderId,
                EsferaIdTerceiro = i.EsferaId
            }).ToList();

            // 3) Obtém empresa (preferências)
            var empresa = await _empresaServico.ObterPorIdAssincrono(empresaId);

            // 4) Aplica filtros de domínio
            var resultado = _recomendacaoServico.FiltrarPorPreferencias(licitacoes, empresa, maxResultados);

            return Ok(resultado);
        }
    }
}
