using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModalidadeController : ControllerBase
    {
        private readonly ModalidadeServico _modalidadeServico;
        private readonly MensagemServico _mensagens;

        public ModalidadeController(ModalidadeServico modalidadeServico, MensagemServico mensagens)
        {
            _modalidadeServico = modalidadeServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarModalidades()
        {
            var sucesso = await _modalidadeServico.ImportarModalidadesAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
