using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class UnidadeServico : BaseTerceiroServico<Unidade>
    {
        private readonly Pncp _pncp;

        public UnidadeServico(IImportacaoTerceiroRepositorio<Unidade> repositorio, MensagemServico mensagens): base(repositorio, mensagens)
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

                if (filtros?.Filters?.Unidades == null || !filtros.Filters.Unidades.Any())
                {
                    Mensagens.Adicionar("Nenhuma unidade encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Unidades)
                {
                    var existente = await Repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        // Não existe ainda - cria uma nova
                        var nova = new Unidade(item.Id, item.Nome, item.Codigo, item.CodigoNome);
                        await Repositorio.AddAsync(nova);
                        novas++;
                    }
                    else
                    {
                        // Já existe - verifica se o nome mudou
                        if (!string.Equals(existente.Nome, item.Nome, StringComparison.OrdinalIgnoreCase) ||
                            !string.Equals(existente.Codigo, item.Codigo, StringComparison.OrdinalIgnoreCase) ||
                            !string.Equals(existente.CodigoNome, item.CodigoNome, StringComparison.OrdinalIgnoreCase))
                        {
                            existente.Nome = item.Nome;
                            existente.Codigo = item.Codigo;
                            existente.CodigoNome = item.CodigoNome;
                            await Repositorio.UpdateAsync(existente);
                            atualizadas++;
                        }
                    }
                }

                await Repositorio.SaveChangesAsync();

                Mensagens.Adicionar(
                    $"Importação concluída. {novas} novas unidades adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {
                Mensagens.Adicionar($"Erro ao importar unidade: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
