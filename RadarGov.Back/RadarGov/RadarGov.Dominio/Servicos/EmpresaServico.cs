using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.IReposoitorio;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Infraestrutura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Servicos
{
    public class EmpresaServico
    {
        private readonly IBaseRepositorio<Empresa> _repositorio;
        private readonly MensagemServico _mensagens;
        private readonly IBaseValidacao _validator;
        public EmpresaServico(IBaseRepositorio<Empresa> repositorio, MensagemServico mensagens, IBaseValidacao validator)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
            _validator = validator;
        }
        

        public async Task<IEnumerable<Empresa>> GetEmpresas()
        {
            try
            {
                _mensagens.Limpar();

                var empresas = await _repositorio.GetAllAsync();

                if (empresas == null)
                {
                    _mensagens.Adicionar("Nenhuma empresa encontrada.", TipoMensagem.Aviso);
                    return empresas;
                }

                else
                {
                    return empresas;
                }
            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao buscar empresa: {ex.Message}", TipoMensagem.Erro);
                return null;
            }
        }

        public async Task<Empresa> GetEmpresa(int id)
        {
            try
            {
                _mensagens.Limpar();

                var empresa = await _repositorio.GetByIdAsync(id);

                if (empresa == null)
                {
                    _mensagens.Adicionar($"Nenhuma empresa com id {id} foi encontrada", TipoMensagem.Aviso);
                    return empresa;
                }

                else
                {
                    return empresa;
                }
            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao buscar empresa: {ex.Message}", TipoMensagem.Erro);
                return null;
            }
        }

        public async Task<bool> PostEmpresa(EmpresaDto empresaDto)
        {
            try
            {
                _mensagens.Limpar();

                var nomeValidado = _validator.ValidateNome(empresaDto.Nome);
                var cnpjValidado = _validator.ValidateCnpj(empresaDto.Cnpj);
                var senhaValidada = _validator.ValidatePassword(empresaDto.Senha);
                var emailValidado = _validator.ValidateEmail(empresaDto.Email);

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

                if (cnpjValidado == false)
                {
                    return false ;
                }

                Empresa empresaNova = new Empresa(empresaDto.Nome, null, empresaDto.Cnpj, empresaDto.Email, empresaDto.Senha, null, null);

                await _repositorio.AddAsync(empresaNova);
                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar("Empresa adicionada com sucesso.", TipoMensagem.Sucesso);
                return true;
            }

            catch(Exception ex)
            {
                _mensagens.Adicionar($"Erro ao adicionar empresa: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

        public async Task<bool> DeleteEmpresa(int id)
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
                _mensagens.Adicionar($"Erro ao deletar empresa: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

        public async Task<bool> UpdateEmpresa(int id, EmpresaDto empresaDto)
        {
            try
            {
                _mensagens.Limpar();

                var nomeValidado = _validator.ValidateNome(empresaDto.Nome);
                var emailValidado = _validator.ValidateEmail(empresaDto.Email);
                var senhaValidada = _validator.ValidatePassword(empresaDto.Senha);
                var cnpjValidada = _validator.ValidateCnpj(empresaDto.Cnpj);

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

                if (cnpjValidada == false)
                {
                    return false;
                }

                var empresaExistente = await _repositorio.GetByIdAsync(id);

                if (empresaExistente == null)
                {
                    _mensagens.Adicionar("Empresa não encontrada.", TipoMensagem.Aviso);
                    return false;
                }

                empresaExistente.Nome = empresaDto.Nome;
                empresaExistente.Email = empresaDto.Email;
                empresaExistente.Senha = empresaDto.Senha;
                empresaExistente.Cnpj = empresaDto.Cnpj;

                await _repositorio.UpdateAsync(empresaExistente);
                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar("Empresa atualizada com sucesso.", TipoMensagem.Sucesso);
                return true;

            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao atualizar empresa: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

    }

    
}
