using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaServico _empresaServico;
        private readonly MensagemServico _mensagens;
        
        public EmpresaController(EmpresaServico empresaServico, MensagemServico mensagens)
        {
            _empresaServico = empresaServico;
            _mensagens = mensagens; 
        }

        [HttpGet("empresa")]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _empresaServico.GetEmpresas();

            return Ok(empresas);

        }

        [HttpGet("empresa/{id}")]

        public async Task<Empresa> GetEmpresa(int id)
        {
            var empresa = await _empresaServico.GetEmpresa(id);

            return empresa;
        }

        [HttpPost("empresa")]

        public async Task<IActionResult> PostEmpresa([FromBody] EmpresaDto empresaDto)
        {
            var response = await _empresaServico.PostEmpresa(empresaDto);
            var mensagens = _mensagens.ObterMensagens();

            if (!response)
            {
                return BadRequest(mensagens);
            }

            else
            {
                return Ok(mensagens);
            }
        }

        [HttpPut("empresa/{id}")]

        public async Task<IActionResult> UdpateEmpresa(int id, [FromBody] EmpresaDto empresaDto)
        {
            var response = await _empresaServico.UpdateEmpresa(id, empresaDto);
            var mensagens = _mensagens.ObterMensagens();

            if (!response)
            {
                return BadRequest(mensagens);
            }

            else
            {
                return Ok(mensagens);
            }
        }

        [HttpDelete("empresa/{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var response = await _empresaServico.DeleteEmpresa(id);
            var mensagens =_mensagens.ObterMensagens();

            if (!response)
            {
                return BadRequest(mensagens);
            }

            else
            {
                return Ok(mensagens);
            }

           
        }
    }
}
