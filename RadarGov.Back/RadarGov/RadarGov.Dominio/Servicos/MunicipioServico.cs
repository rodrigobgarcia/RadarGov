using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.DTOs;

namespace RadarGov.Dominio.Servicos
{
    public class MunicipioServico
    {
        private readonly IImportacaoTerceiroRepositorio<Municipio> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public MunicipioServico(
            IImportacaoTerceiroRepositorio<Municipio> repositorio,
            MensagemServico mensagens)
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

                if (filtros?.Filters?.Municipios == null || !filtros.Filters.Municipios.Any())
                {
                    _mensagens.Adicionar("Nenhum municipio encontrado para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;
                int atualizadas = 0;

                foreach (var item in filtros.Filters.Municipios)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);

                    if (existente == null)
                    {
                        // Não existe ainda - cria uma nova
                        var nova = new Municipio(item.Id, item.Nome);
                        await _repositorio.AddAsync(nova);
                        novas++;
                    }
                    else
                    {
                        // Já existe - verifica se o nome mudou
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
                    $"Importação concluída. {novas} novas municipio adicionadas e {atualizadas} atualizadas.",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {
                _mensagens.Adicionar($"Erro ao importar municipio: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }

    }
}
