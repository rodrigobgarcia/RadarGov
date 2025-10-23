using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;

namespace RadarGov.Dominio.Servicos
{
    public class OrgaoServico
    {
        private readonly IImportacaoTerceiroRepositorio<Orgao> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public OrgaoServico(IImportacaoTerceiroRepositorio<Orgao> repositorio, MensagemServico mensagens)
        {
            _repositorio = repositorio;
            _pncp = new Pncp();
            _mensagens = mensagens;
        }

        public async Task<bool> ImportarAsync()
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

                if (filtros?.Filters?.Orgaos == null || !filtros.Filters.Orgaos.Any())
                {
                    _mensagens.Adicionar("Nenhuma modalidade encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Orgaos)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        // Não existe ainda - cria uma nova
                        var nova = new Orgao(item.Id, item.Nome, item.Cnpj);
                        await _repositorio.AddAsync(nova);
                        novas++;
                    }
                    else
                    {
                        // Já existe - verifica se o nome mudou
                        if (!string.Equals(existente.Nome, item.Nome, StringComparison.OrdinalIgnoreCase) ||
                            !string.Equals(existente.Cnpj, item.Cnpj, StringComparison.OrdinalIgnoreCase))
                        {
                            existente.Nome = item.Nome;
                            existente.Cnpj = item.Cnpj;
                            await _repositorio.UpdateAsync(existente);
                            atualizadas++;
                        }
                    }
                }

                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar(
                    $"Importação concluída. {novas} novas modalidades adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao importar modalidades: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}
