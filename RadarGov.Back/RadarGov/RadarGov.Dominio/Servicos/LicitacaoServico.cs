using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Entidades;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Integracoes.Pnc;


namespace RadarGov.Dominio.Servicos
{
    public class LicitacaoServico
    {
        private readonly IImportacaoTerceiroRepositorio<Licitacao> _repositorio;
        private readonly Pncp _pncp;
        private readonly MensagemServico _mensagens;

        private readonly OrgaoServico _orgaoServico;
        private readonly MunicipioServico _municipioServico;
        private readonly ModalidadeServico _modalidadeServico;
        private readonly TipoServico _tipoServico;
        private readonly FonteOrcamentariaServico _fonteOrcamentariaServico;
        private readonly UfsServico _ufsServico;


        public LicitacaoServico(
            IImportacaoTerceiroRepositorio<Licitacao> repositorio,
            MensagemServico mensagens,
            OrgaoServico orgaoServico,
            MunicipioServico municipioServico,
            ModalidadeServico modalidadeServico,
            TipoServico tipoServico,
            FonteOrcamentariaServico fonteOrcamentariaServico,
            UfsServico ufsServico)
        {
            _repositorio = repositorio;
            _pncp = new Pncp();
            _mensagens = mensagens;

            _orgaoServico = orgaoServico;
            _municipioServico = municipioServico;
            _modalidadeServico = modalidadeServico;
            _tipoServico = tipoServico;
            _fonteOrcamentariaServico = fonteOrcamentariaServico;
            _ufsServico = ufsServico;
        }

        private async Task<Licitacao?> MapearLicitacaoAsync(LicitacaoResponse item, Licitacao? existente)
        {
            var licitacao = existente ?? new Licitacao { IdTerceiro = item.Id };
            bool dadosMestresCompletos = true;


            var orgao = await _orgaoServico.ObterPorIdTerceiroAsync(item.OrgaoId);
            if (orgao == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.OrgaoId = orgao.Id;
            }

            var municipio = await _municipioServico.ObterPorIdTerceiroAsync(item.MunicipioId);
            if (municipio == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.MunicipioId = municipio.Id;
            }

            var uf = await _ufsServico.ObterPorIdTerceiroAsync(item.Uf);
            if (uf == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.UfId = uf.Id;
            }

            var modalidade = await _modalidadeServico.ObterPorIdTerceiroAsync(item.ModalidadeLicitacaoId);
            if (modalidade == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.ModalidadeId = modalidade.Id;
            }

            var tipo = await _tipoServico.ObterPorIdTerceiroAsync(item.TipoId);
            if (tipo == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.TipoId = tipo.Id;
            }

 
            var fonte = await _fonteOrcamentariaServico.ObterPorIdTerceiroAsync(item.FonteOrcamentariaId);
            if (fonte == null)
            {
                dadosMestresCompletos = false;
            }
            else
            {
                licitacao.FonteOrcamentariaId = fonte.Id;
            }

            if (!dadosMestresCompletos)
            {
                return null;
            }

            licitacao.Titulo = item.Title;
            licitacao.Descricao = item.Description;
            licitacao.ItemUrl = item.ItemUrl;
            licitacao.Ano = item.Ano;
            licitacao.UltimaAlteracao = DateTime.Now;
            licitacao.DataPublicacaoPncp = item.DataPublicacaoPncp;
            licitacao.DataAtualizacaoPncp = item.DataAtualizacaoPncp;
            licitacao.DataInicioVigencia = item.DataInicioVigencia;
            licitacao.DataFimVigencia = item.DataFimVigencia;
            licitacao.ValorGlobal = item.ValorGlobal;
            licitacao.TemResultado = item.TemResultado;
            licitacao.OrgaoSubrogadoId = item.OrgaoSubrogadoId;
            licitacao.OrgaoSubrogadoNome = item.OrgaoSubrogadoNome;
            licitacao.NumeroSequencial = item.NumeroSequencial;
            licitacao.NumeroControlePncp = item.NumeroControlePncp;
            licitacao.ExigenciaConteudoNacional = item.ExigenciaConteudoNacional;
            licitacao.TemResultado = item.TemResultado;
            licitacao.NumeroSequencialCompraAta = item.NumeroSequencialCompraAta;



            return licitacao;
        }

        public async Task<bool> ImportarAsync()
        {
            try
            {
                _mensagens.Limpar();
                var licitacoesResponse = await _pncp.GetTodasLicitacoesRecebendoProposta();

                if (licitacoesResponse == null || !licitacoesResponse.Any())
                {
                    _mensagens.Adicionar("Nenhuma licitação encontrada para importação.", TipoMensagem.Aviso);
                    return true;
                }

                int novas = 0;
                int atualizadas = 0;
                int ignoradas = 0;

                foreach (var item in licitacoesResponse)
                {
                    var existente = await _repositorio.ObterPorIdTerceiroAsync(item.Id);
                    var licitacaoFinal = await MapearLicitacaoAsync(item, existente);

                    if (licitacaoFinal == null)
                    {
                        ignoradas++;
                        continue;
                    }

                    if (existente == null)
                    {
                        await _repositorio.AddAsync(licitacaoFinal);
                        novas++;
                    }
                    else
                    {
                        if (existente.DataAtualizacaoPncp != item.DataAtualizacaoPncp)
                        {
                            await _repositorio.UpdateAsync(licitacaoFinal);
                            atualizadas++;
                        }
                    }
                }

                await _repositorio.SaveChangesAsync();

                _mensagens.Adicionar(
                    $"Importação concluída. {novas} novas licitações, {atualizadas} atualizadas e {ignoradas} ignoradas (por dados mestres).",
                    TipoMensagem.Sucesso);

                return true;
            }
            catch (Exception ex)
            {

                _mensagens.Adicionar($"Erro grave ao processar a importação de licitações: {ex.Message}", TipoMensagem.Erro);
                return false;
            }
        }
    }
}