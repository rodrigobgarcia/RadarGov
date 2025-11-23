using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Interfaces;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcoesController : ControllerBase
    {
        private readonly IRadarHubIntegracaoServico _radarHubServico;

        public OpcoesController(IRadarHubIntegracaoServico radarHubServico)
        {
            _radarHubServico = radarHubServico;
        }


        [HttpGet("modalidades")]
        public async Task<IActionResult> GetModalidades([FromQuery] string odata = "")
        {
            var modalidades = await _radarHubServico.ObterModalidadesAsync(odata);
            return Ok(modalidades);
        }


        [HttpGet("orgaos")]
        public async Task<IActionResult> GetOrgaos([FromQuery] string odata = "")
        {
            var orgaos = await _radarHubServico.ObterOrgaosAsync(odata);
            return Ok(orgaos);
        }


        [HttpGet("municipios")]
        public async Task<IActionResult> GetMunicipios([FromQuery] string odata = "")
        {
            var municipios = await _radarHubServico.ObterMunicipiosAsync(odata);
            return Ok(municipios);
        }


        [HttpGet("ufs")]
        public async Task<IActionResult> GetUfs([FromQuery] string odata = "")
        {
            var ufs = await _radarHubServico.ObterUfsAsync(odata);
            return Ok(ufs);
        }


        [HttpGet("segmentos")]
        public async Task<IActionResult> GetSegmentos([FromQuery] string odata = "")
        {
            var segmentos = await _radarHubServico.ObterSegmentoAsync(odata);
            return Ok(segmentos);
        }


        [HttpGet("tipos-margem-preferencia")]
        public async Task<IActionResult> GetTiposMargemPreferencia([FromQuery] string odata = "")
        {
            var tipos = await _radarHubServico.ObterTipoMargemPreferenciaAsync(odata);
            return Ok(tipos);
        }


        [HttpGet("poderes")]
        public async Task<IActionResult> GetPoderes([FromQuery] string odata = "")
        {
            var poderes = await _radarHubServico.ObterPoderAsync(odata);
            return Ok(poderes);
        }


        [HttpGet("unidades")]
        public async Task<IActionResult> GetUnidades([FromQuery] string odata = "")
        {
            var unidades = await _radarHubServico.ObterUnidadeAsync(odata);
            return Ok(unidades);
        }


        [HttpGet("tipos")]
        public async Task<IActionResult> GetTipos([FromQuery] string odata = "")
        {
            var tipos = await _radarHubServico.ObterTipoAsync(odata);
            return Ok(tipos);
        }
    }
}
