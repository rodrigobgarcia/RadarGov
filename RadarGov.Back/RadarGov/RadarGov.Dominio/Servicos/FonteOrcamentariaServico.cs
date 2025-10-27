using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class FonteOrcamentariaServico : BaseTerceiroServico<FonteOrcamentaria>
    {
        private readonly Pncp _pncp;

        public FonteOrcamentariaServico (IImportacaoTerceiroRepositorio<FonteOrcamentaria> repositorio, MensagemServico mensagens): base(repositorio, mensagens)
        {
            this._pncp = new Pncp();
        }

        public async Task<bool> ImportarAsync()
        {
            try
            {
                Mensagens.Limpar();

                var json = await _pncp.GetFiltros();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json, options);

                if (filtros?.Filters?.FontesOrcamentarias ==  null || !filtros.Filters.FontesOrcamentarias.Any())
                {
                    Mensagens.Adicionar($"Nenhuma fonte orçamentária encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.FontesOrcamentarias)
                {
                    var existente = await Repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        var nova = new FonteOrcamentaria(item.Id, item.Nome);
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
                    $"Importação concluída. {novas} fontes orçamentárias adicionas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;

            }
            catch (Exception ex)
            {
                Mensagens.Adicionar($"Erro ao importar fontes orçamentárias: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
