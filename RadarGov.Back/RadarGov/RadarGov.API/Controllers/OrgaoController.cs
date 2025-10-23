using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrgaoController : ControllerBase
    {
        private readonly OrgaoServico _orgaoServico;
        private readonly MensagemServico _mensagens;

        public OrgaoController(OrgaoServico orgaoServico, MensagemServico mensagens)
        {
            _orgaoServico = orgaoServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarModalidades()
        {
            var sucesso = await _orgaoServico.ImportarAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
