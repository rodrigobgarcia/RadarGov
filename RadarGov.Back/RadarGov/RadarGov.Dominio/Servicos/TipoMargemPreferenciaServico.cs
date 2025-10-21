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
    public class TipoMargemPreferenciaServico
    {
        private readonly IImportacaoTerceiroRepositorio<TipoMargemPreferencia> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public TipoMargemPreferenciaServico(IImportacaoTerceiroRepositorio<TipoMargemPreferencia> tipoMargemPreferencia, MensagemServico mensagens)
        {
            _repositorio = tipoMargemPreferencia;
            _pncp = new Pncp();
            _mensagens = mensagens; 
        }

        public async Task<bool> ImportarTipoMargemPreferenciaAsync()
        {
            try
            {
                _mensagens.Limpar();

                var json = await _pncp.GetFiltros();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json, options) ;

                if (filtros?.Filters.Modalidades == null || !filtros.Filters.Modalidades.Any())
                {
                    _mensagens.Adicionar("Nenhuma modalidade encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.TiposMargensPreferencia)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente != null)
                    {
                        var nova = new TipoMargemPreferencia(item.Id, item.Nome);

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
                    $"Importação concluída. {novas} novas tipos de margens preferência adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;

            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao importar tipos de margem preferência: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
