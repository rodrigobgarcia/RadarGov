using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.DTOs;

namespace RadarGov.Dominio.Servicos
{
    public class ModalidadeServico
    {
        private readonly IImportacaoTerceiroRepositorio<Modalidade> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        public ModalidadeServico(
            IImportacaoTerceiroRepositorio<Modalidade> repositorio,
            MensagemServico mensagens)
        {
            _repositorio = repositorio;
            _pncp = new Pncp();
            _mensagens = mensagens;
        }

        public async Task<bool> ImportarModalidadesAsync()
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


                if (filtros?.Filters?.Modalidades == null || !filtros.Filters.Modalidades.Any())
                {
                    _mensagens.Adicionar("Nenhuma modalidade encontrada para importação.", TipoMensagem.Aviso);
                    return false;
                }

                int novas = 0;

                foreach (var item in filtros.Filters.Modalidades)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);
                    if (existente == null)
                    {
                        var nova = new Modalidade(item.Id, item.Nome);
                        await _repositorio.AddAsync(nova);
                        novas++;
                    }
                }

                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar($"Importação concluída. {novas} novas modalidades adicionadas.", TipoMensagem.Sucesso);

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
