using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class EsferaServico : BaseTerceiroServico<Esfera>
    {
        private readonly Pncp _pncp;

        public EsferaServico(IImportacaoTerceiroRepositorio<Esfera> esferaRepositorio, MensagemServico mensagens): base(esferaRepositorio, mensagens)
        {
            _pncp = new Pncp();
        }

        public async Task<bool> ImportarEsferasaAsync()
        {
            try
            {
                Mensagens.Limpar();

                var json = await _pncp.GetFiltros();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json, options);

                if (filtros?.Filters.Esferas == null || !filtros.Filters.Esferas.Any())
                {
                    Mensagens.Adicionar("Nenhuma esfera encontrada para importação.", TipoMensagem.Aviso);
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Esferas)
                {
                    var existente = await Repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        var nova = new Esfera(item.Id, item.Nome);
                        await Repositorio.AddAsync(nova);
                        novas++;
                    }

                    else
                    {
                        if (!string.Equals(existente.Nome, item.Nome, StringComparison.OrdinalIgnoreCase))
                        {
                            existente.Nome = item.Nome;
                            await Repositorio.UpdateAsync(existente);
                            atualizadas++;
                        }
                    }
                }

                await Repositorio.SaveChangesAsync();

                Mensagens.Adicionar(
                    $"Importação concluída. {novas} novas esferas adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }

            catch (Exception ex)
            {
                Mensagens.Adicionar($"Erro ao importar esferas: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
