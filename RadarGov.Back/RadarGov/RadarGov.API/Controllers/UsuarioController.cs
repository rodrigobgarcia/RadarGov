using Microsoft.AspNetCore.Mvc;
using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.Servicos;

namespace RadarGov.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServico _usuarioServico;
        private readonly MensagemServico _mensagens;

        public UsuarioController(UsuarioServico usuarioServico, MensagemServico mensagens)
        {
            _usuarioServico = usuarioServico;
            _mensagens = mensagens;
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioServico.GetUsuarios();
            var mensagens = _mensagens.ObterMensagens();

            if (usuarios == null)
            {
                return Ok(mensagens);
            }

            else
            {
                return Ok(usuarios);
            }
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioServico.GetUsuario(id);
            var mensagens = _mensagens.ObterMensagens();

            if (usuario ==  null)
            {
                return NotFound(mensagens);
            }

            else
            {
                return Ok(usuario);
            }
        }

        [HttpPost("usuario")]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var response = await _usuarioServico.PostUsuario(usuarioDto);
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

        [HttpPut("usuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            var response = await _usuarioServico.UpdateUsuario(id, usuarioDto);
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

        [HttpDelete("usuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var response = await _usuarioServico.DeleteUsuario(id);
            var mensagens = _mensagens.ObterMensagens();

            if (!response)
            {
                return NotFound(mensagens);
            }

            else
            {
                return Ok(mensagens);
            }
        }
    }
}
