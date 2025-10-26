using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.IReposoitorio;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Servicos
{
    public class UsuarioServico
    {
        private readonly IBaseRepositorio<Usuario> _repositorio;
        private readonly MensagemServico _mensagens;
        private readonly IBaseValidacao _validator;

        public UsuarioServico(IBaseRepositorio<Usuario> repositorio, MensagemServico mensagens, IBaseValidacao validator)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
            _validator = validator;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            try
            {
                _mensagens.Limpar();

                var usuarios = await _repositorio.GetAllAsync();

                if (usuarios == null)
                {
                    _mensagens.Adicionar("Nenhum usuario foi encontrado", TipoMensagem.Aviso);
                    return usuarios;
                }

                else
                {
                    return usuarios;
                }

            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao buscar usuários: {ex.Message}", TipoMensagem.Erro);
                return null;
            }

        }

        public async Task<Usuario> GetUsuario(int id)
        {
            try
            {
                _mensagens.Limpar();

                var usuario = await _repositorio.GetByIdAsync(id);

                if (usuario == null)
                {
                    _mensagens.Adicionar("Nenhum usuario foi encontrado com o este Id", TipoMensagem.Aviso);
                    return usuario;
                }

                else
                {
                    return usuario;
                }
            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao buscar usuario: {ex.Message}", TipoMensagem.Erro);
                return null;
            }
        }

        public async Task<bool> PostUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                _mensagens.Limpar();

                var nomeValidado = _validator.ValidateNome(usuarioDto.Nome);
                var emailValidado = _validator.ValidateEmail(usuarioDto.Email);
                var senhaValidada = _validator.ValidatePassword(usuarioDto.Senha);

                if (nomeValidado == false)
                {
                    return false;
                }

                if (emailValidado == false)
                {
                    return false;
                }

                if (senhaValidada == false)
                {
                    return false;
                }

                else
                {
                    Usuario novoUsuario = new Usuario(usuarioDto.Nome, usuarioDto.Email, usuarioDto.Senha, null, null);

                    await _repositorio.AddAsync(novoUsuario);
                    await _repositorio.SaveChangesAsync();

                    _mensagens.Adicionar("Usuario adicionado com sucesso.", TipoMensagem.Sucesso);

                    return true;

                }
            }

            catch (Exception ex) 
            {
                _mensagens.Adicionar($"Erro ao adicionar usuario: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            try
            {
                _mensagens.Limpar();

                await _repositorio.DeleteAsync(id);
                await _repositorio.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao deletar usuario: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

        public async Task<bool> UpdateUsuario(int id, UsuarioDto usuarioDto)
        {
            try
            {
                _mensagens.Limpar();

                var nomeValidado = _validator.ValidateNome(usuarioDto.Nome);
                var emailValidado = _validator.ValidateEmail(usuarioDto.Email);
                var senhaValidada = _validator.ValidatePassword(usuarioDto.Senha);

                if (nomeValidado == false)
                {
                    return false;
                }

                if (emailValidado == false)
                {
                    return false;
                }

                if (senhaValidada == false)
                {
                    return false;
                }

                var usuarioExistente = await _repositorio.GetByIdAsync(id);

                if (usuarioExistente == null)
                {
                    _mensagens.Adicionar("Usuario não encontrado.", TipoMensagem.Aviso);
                    return false;
                }

                usuarioExistente.Nome = usuarioDto.Nome;
                usuarioExistente.Email = usuarioDto.Email;
                usuarioExistente.Senha = usuarioDto.Senha;

                await _repositorio.UpdateAsync(usuarioExistente);
                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar("Usuario atualizado com sucesso.", TipoMensagem.Sucesso);
                return true;

            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao atualizar usuario: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
