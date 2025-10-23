using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MunicipioController : ControllerBase
    {
        private readonly MunicipioServico _municipioServico;
        private readonly MensagemServico _mensagens;

        public MunicipioController(MunicipioServico municipioServico, MensagemServico mensagens)
        {
            _municipioServico = municipioServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarMunicipios()
        {
            var sucesso = await _municipioServico.ImportarAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
