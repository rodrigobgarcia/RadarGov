using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;


namespace RadarGov.Dominio.Servicos
{
    public class OrgaoServico : BaseTerceiroServico<Orgao>
    {
        private readonly Pncp _pncp;

        public OrgaoServico(IImportacaoTerceiroRepositorio<Orgao> orgaoRepositorio, MensagemServico mensagens) : base(orgaoRepositorio, mensagens)
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

                if(filtros?.Filters?.Orgaos == null || !filtros.Filters.Orgaos.Any())
                {
                    Mensagens.Adicionar("Nenhum orgão encontrado para importação", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Orgaos)
                {
                    var existente = await Repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        var nova = new Orgao(item.Id, item.Nome, item.Cnpj);
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
                    $"Importação concluída. {novas} novos orgãos adicionados e {atualizadas} atualizado.",
                    TipoMensagem.Sucesso);

                return true;


            }
            catch(Exception ex)
            {
                Mensagens.Adicionar($"Erro ao importar orgão: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
