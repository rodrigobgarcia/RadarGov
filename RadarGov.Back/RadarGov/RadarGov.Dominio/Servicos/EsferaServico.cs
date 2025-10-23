using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class EsferaServico
    {
        private readonly IImportacaoTerceiroRepositorio<Esfera> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public EsferaServico(IImportacaoTerceiroRepositorio<Esfera> esferaRepositorio, MensagemServico mensagens)
        {
            _repositorio = esferaRepositorio;
            _pncp = new Pncp();
            _mensagens = mensagens;
        }

        public async Task<bool> ImportarEsferasaAsync()
        {
            try
            {
                _mensagens.Limpar();

                var json = await _pncp.GetFiltros();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json, options);

                if (filtros?.Filters.Esferas == null || !filtros.Filters.Esferas.Any())
                {
                    _mensagens.Adicionar("Nenhuma esfera encontrada para importação.", TipoMensagem.Aviso);
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Esferas)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        var nova = new Esfera(item.Id, item.Nome);
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
                    $"Importação concluída. {novas} novas esferas adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }

            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao importar esferas: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
