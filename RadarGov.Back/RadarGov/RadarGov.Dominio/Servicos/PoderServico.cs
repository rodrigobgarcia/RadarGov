using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class PoderServico : BaseTerceiroServico<Poder>
    {
        private readonly Pncp _pncp;

        public PoderServico(
            IImportacaoTerceiroRepositorio<Poder> repositorio,
            MensagemServico mensagens) : base(repositorio, mensagens)
        {
            _pncp = new Pncp();
        }

        public async Task<bool> ImportarAsync()
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

                if (filtros?.Filters?.Poderes == null || !filtros.Filters.Poderes.Any())
                {
                    Mensagens.Adicionar("Nenhuma modalidade encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Poderes)
                {
                    var existente = await Repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        // Não existe ainda - cria uma nova
                        var nova = new Poder(item.Id, item.Nome);
                        await Repositorio.AddAsync(nova);
                        novas++;
                    }
                    else
                    {
                        // Já existe - verifica se o nome mudou
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
                    $"Importação concluída. {novas} novas poderes adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {
                Mensagens.Adicionar($"Erro ao importar poderes: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

    }
}
