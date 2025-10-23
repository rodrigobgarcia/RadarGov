using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoderController : ControllerBase
    {
        private readonly PoderServico _poderServico;
        private readonly MensagemServico _mensagens;

        public PoderController(PoderServico poderServico, MensagemServico mensagens)
        {
            _poderServico = poderServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarModalidades()
        {
            var sucesso = await _poderServico.ImportarAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
