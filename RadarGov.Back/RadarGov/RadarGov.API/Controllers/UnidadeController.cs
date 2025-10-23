using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadeController : ControllerBase
    {
        private readonly UnidadeServico _unidadeServico;
        private readonly MensagemServico _mensagens;

        public UnidadeController(UnidadeServico unidadeServico, MensagemServico mensagens)
        {
            _unidadeServico = unidadeServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarModalidades()
        {
            var sucesso = await _unidadeServico.ImportarAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
