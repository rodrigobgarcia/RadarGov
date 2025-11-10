using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Interfaces;

namespace RadarGov.API.Controllers
{
    [Route("api/[controller]")]
    public class OpcoesController : ODataController
    {
        private readonly IRadarHubIntegracaoServico _radarHubServico;

        public OpcoesController(IRadarHubIntegracaoServico radarHubServico)
        {
            _radarHubServico = radarHubServico;
        }

        [HttpGet("modalidades")]
        [EnableQuery]
        public async Task<IQueryable<ModalidadeDto>> GetModalidades()
        {
            var modalidades = await _radarHubServico.ObterModalidades();
            return modalidades.AsQueryable();
        }

        [HttpGet("orgaos")]
        [EnableQuery]
        public async Task<IQueryable<OrgaoDto>> GetOrgaos()
        {
            var orgaos = await _radarHubServico.ObterOrgaos();
            return orgaos.AsQueryable();
        }

        [HttpGet("municipios")]
        [EnableQuery]
        public async Task<IQueryable<MunicipioDto>> GetMunicipios([FromQuery] string uf = null)
        {
            var municipios = await _radarHubServico.ObterMunicipios(uf);
            return municipios.AsQueryable();
        }

        [HttpGet("ufs")]
        [EnableQuery]
        public async Task<IQueryable<UfDto>> GetUfs()
        {
            var ufs = await _radarHubServico.ObterUfs();
            return ufs.AsQueryable();
        }
    }
}