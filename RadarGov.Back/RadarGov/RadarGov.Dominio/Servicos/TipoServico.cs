using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Servicos
{
    public class TipoServico
    {
        private readonly IImportacaoTerceiroRepositorio<Tipo> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public TipoServico(IImportacaoTerceiroRepositorio<Tipo> repositorio, MensagemServico mensagens)
        {
            this._repositorio = repositorio;
            this._pncp = new Pncp();
            this._mensagens = mensagens;
        }

        public async Task<bool> ImportarAsync()
        {
            try
            {
                _mensagens.Limpar();

                var json = await _pncp.GetFiltros();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json, options);

                if (filtros?.Filters?.Tipos ==  null || !filtros.Filters.Tipos.Any())
                {
                    _mensagens.Adicionar($"Nenhum tipo encontrado para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Tipos)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        var nova = new Tipo(item.Id, item.Nome);
                        await _repositorio.AddAsync(nova);
                        novas++;
                    }
                    else
                    {
                        if (!string.Equals(existente.Nome, item.Nome, StringComparison.OrdinalIgnoreCase))
                        {
                            existente.Nome = item.Nome;
                            await _repositorio.UpdateAsync(existente);
                            atualizadas++;
                        }
                    }
                }

                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar(
                    $"Importação concluída. {novas} novos tipos adiconados e {atualizadas} atualizados.",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao importar tipos: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
