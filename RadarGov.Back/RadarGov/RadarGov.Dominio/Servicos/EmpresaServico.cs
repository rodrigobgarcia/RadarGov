using RadarGov.Dominio.Entidades;
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
        public EmpresaServico(IBaseRepositorio<Empresa> repositorio, MensagemServico mensagens)
        {
            _repositorio = repositorio;
            _mensagens = mensagens;
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
    }

    
}
