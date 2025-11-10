using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RSK.Dominio.Interfaces;

namespace RadarGov.Dominio.Servicos
{
    // Serviço para recomendar licitações com base nas preferências da empresa
    public class LicitacaoRecomendacaoServico
    {
        private readonly EmpresaServico _empresaServico;
        private readonly IServicoConsultaBase<Licitacao> _licitacaoConsulta;

        public LicitacaoRecomendacaoServico(
            EmpresaServico empresaServico,
            IServicoConsultaBase<Licitacao> licitacaoConsulta)
        {
            _empresaServico = empresaServico;
            _licitacaoConsulta = licitacaoConsulta;
        }

        /// <summary>
        /// Filtra e recomenda licitações que correspondem às preferências da empresa.
        /// </summary>
        /// <param name="empresaId">ID da empresa para a qual as licitações serão recomendadas.</param>
        /// <param name="maxResultados">Número máximo de licitações a retornar.</param>
        public async Task<IEnumerable<Licitacao>> RecomendarLicitacoesAsync(long empresaId, int maxResultados = 50)
        {
            var empresa = await _empresaServico.ObterPorIdAssincrono(empresaId);

            if (empresa == null)
            {
                return Enumerable.Empty<Licitacao>();
            }

            // A consulta base (IQueryable) de todas as licitações
            var query = _licitacaoConsulta.ObterIQueryable();

            // 1. FILTRO DE ÓRGÃOS (ORGAOS)
            if (empresa.OrgaosIdTerceiroPreferidos != null && empresa.OrgaosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.OrgaosIdTerceiroPreferidos.Contains(l.OrgaoIdTerceiro));
            }

            // 2. FILTRO DE MUNICÍPIOS
            if (empresa.MunicipiosIdTerceiroPreferidos != null && empresa.MunicipiosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.MunicipiosIdTerceiroPreferidos.Contains(l.MunicipioIdTerceiro));
            }

            // 3. FILTRO DE MODALIDADES
            if (empresa.ModalidadesIdTerceiroPreferidas != null && empresa.ModalidadesIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.ModalidadesIdTerceiroPreferidas.Contains(l.ModalidadeIdTerceiro));
            }

            // 4. FILTRO DE TIPOS
            if (empresa.TiposIdTerceiroPreferidos != null && empresa.TiposIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.TiposIdTerceiroPreferidos.Contains(l.TipoIdTerceiro));
            }

            // 5. FILTRO DE UFs
            if (empresa.UfsIdTerceiroPreferidas != null && empresa.UfsIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.UfsIdTerceiroPreferidas.Contains(l.UfIdTerceiro));
            }

            // 6. FILTRO DE PODERES
            if (empresa.PoderesIdTerceiroPreferidos != null && empresa.PoderesIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.PoderesIdTerceiroPreferidos.Contains(l.PoderIdTerceiro));
            }

            // 7. FILTRO DE CONTEÚDO NACIONAL
            if (empresa.PrefereExigenciaConteudoNacional)
            {
                // Se a empresa prefere conteúdo nacional, filtramos apenas as licitações que o exigem
                query = query.Where(l => l.ExigenciaConteudoNacional);
            }

            // Adiciona ordenação (ex: pela data mais recente) e limite de resultados
            var licitacoesRecomendadas = await Task.FromResult(
                query.OrderByDescending(l => l.DataPublicacaoPncp)
                     .Take(maxResultados)
                     .ToList()); // Converte para lista e executa a query no banco de dados

            return licitacoesRecomendadas;
        }

        /// <summary>
        /// Filtra uma coleção de licitações (já carregadas) usando as preferências da empresa.
        /// Retorna os top <paramref name="maxResultados"/> ordenados por DataPublicacaoPncp desc.
        /// </summary>
        public IEnumerable<Licitacao> FiltrarPorPreferencias(IEnumerable<Licitacao> licitacoes, Empresa empresa, int maxResultados = 50)
        {
            if (licitacoes == null) return Enumerable.Empty<Licitacao>();
            if (empresa == null) return licitacoes.OrderByDescending(l => l.DataPublicacaoPncp).Take(maxResultados);

            var query = licitacoes.AsQueryable();

            if (empresa.OrgaosIdTerceiroPreferidos != null && empresa.OrgaosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.OrgaosIdTerceiroPreferidos.Contains(l.OrgaoIdTerceiro));
            }

            if (empresa.MunicipiosIdTerceiroPreferidos != null && empresa.MunicipiosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.MunicipiosIdTerceiroPreferidos.Contains(l.MunicipioIdTerceiro));
            }

            if (empresa.ModalidadesIdTerceiroPreferidas != null && empresa.ModalidadesIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.ModalidadesIdTerceiroPreferidas.Contains(l.ModalidadeIdTerceiro));
            }

            if (empresa.TiposIdTerceiroPreferidos != null && empresa.TiposIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.TiposIdTerceiroPreferidos.Contains(l.TipoIdTerceiro));
            }

            if (empresa.UfsIdTerceiroPreferidas != null && empresa.UfsIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.UfsIdTerceiroPreferidas.Contains(l.UfIdTerceiro));
            }

            if (empresa.PoderesIdTerceiroPreferidos != null && empresa.PoderesIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.PoderesIdTerceiroPreferidos.Contains(l.PoderIdTerceiro));
            }

            if (empresa.PrefereExigenciaConteudoNacional)
            {
                query = query.Where(l => l.ExigenciaConteudoNacional);
            }

            return query.OrderByDescending(l => l.DataPublicacaoPncp).Take(maxResultados).ToList();
        }
    }
}