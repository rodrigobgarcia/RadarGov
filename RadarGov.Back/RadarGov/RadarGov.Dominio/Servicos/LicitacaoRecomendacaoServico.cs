using RadarGov.Dominio.Entidades;
using RSK.Dominio.Interfaces;

namespace RadarHub.Dominio.Servicos
{
    // Interface que o serviço de empresa deve implementar (assumido)
    public interface IEmpresaServico
    {
        Task<Empresa> ObterPorIdAssincrono(long id);
    }

    // Serviço para recomendar licitações com base nas preferências da empresa
    public class LicitacaoRecomendacaoServico
    {
        private readonly IEmpresaServico _empresaServico;
        private readonly IServicoConsultaBase<Licitacao> _licitacaoConsulta;

        public LicitacaoRecomendacaoServico(
            IEmpresaServico empresaServico,
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
                // Poderia ser um throw ou retornar uma lista vazia, dependendo da regra de negócio.
                return Enumerable.Empty<Licitacao>();
            }

            // A consulta base (IQueryable) de todas as licitações
            var query = _licitacaoConsulta.ObterIQueryable();

            // 1. FILTRO DE ÓRGÃOS (ORGAOS)
            if (empresa.OrgaosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.OrgaosIdTerceiroPreferidos.Contains(l.OrgaoIdTerceiro));
            }

            // 2. FILTRO DE MUNICÍPIOS
            if (empresa.MunicipiosIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.MunicipiosIdTerceiroPreferidos.Contains(l.MunicipioIdTerceiro));
            }

            // 3. FILTRO DE MODALIDADES
            if (empresa.ModalidadesIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.ModalidadesIdTerceiroPreferidas.Contains(l.ModalidadeIdTerceiro));
            }

            // 4. FILTRO DE TIPOS
            if (empresa.TiposIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.TiposIdTerceiroPreferidos.Contains(l.TipoIdTerceiro));
            }

            // 5. FILTRO DE UFs
            if (empresa.UfsIdTerceiroPreferidas.Any())
            {
                query = query.Where(l => empresa.UfsIdTerceiroPreferidas.Contains(l.UfIdTerceiro));
            }

            // 6. FILTRO DE PODERES
            if (empresa.PoderesIdTerceiroPreferidos.Any())
            {
                query = query.Where(l => empresa.PoderesIdTerceiroPreferidos.Contains(l.PoderIdTerceiro));
            }

            // 7. FILTRO DE CONTEÚDO NACIONAL
            if (empresa.PrefereExigenciaConteudoNacional)
            {
                // Se a empresa prefere conteúdo nacional, filtramos apenas as licitações que o exigem
                query = query.Where(l => l.ExigenciaConteudoNacional);
            }
            // NOTA: Para aumentar a complexidade da recomendação, filtros como Fonte Orcamentária
            // e Tipo Margem Preferencia podem ser adicionados aqui seguindo o mesmo padrão.
            // Exemplo: 
            // if (empresa.FontesOrcamentariasIdTerceiroPreferidas.Any()) { ... }


            // Adiciona ordenação (ex: pela data mais recente) e limite de resultados
            var licitacoesRecomendadas = await Task.FromResult(
                query.OrderByDescending(l => l.DataPublicacaoPncp)
                     .Take(maxResultados)
                     .ToList()); // Converte para lista e executa a query no banco de dados

            return licitacoesRecomendadas;
        }
    }
}