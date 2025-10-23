using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.Servicos;
using RadarGov.Dominio.Notificacoes.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UfsController : ControllerBase
    {
        private readonly UfsServico _ufsServico;
        private readonly MensagemServico _mensagens;

        public UfsController(UfsServico ufsServico, MensagemServico mensagens)
        {
            _ufsServico = ufsServico;
            _mensagens = mensagens;
        }


        [HttpPost("importar")]
        public async Task<IActionResult> ImportarModalidades()
        {
            var sucesso = await _ufsServico.ImportarAsync();
            var resultado = _mensagens.ObterMensagens();

            if (!sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
