using RadarGov.Dominio.Entidades;

namespace RadarGov.Dominio.Servicos
{
    public class SegmentoServico
    {
        private readonly Dictionary<string, Segmento> _segmentos;
        private readonly Dictionary<string, List<string>> _termosChave;

        public SegmentoServico()
        {
            
        }

        /// <summary>
        /// Tenta classificar o segmento de uma licitação com base em Termos-Chave.
        /// </summary>
        /// <param name="titulo">Título da licitação.</param>
        /// <param name="descricao">Descrição da licitação.</param>
        /// <returns>O ID do Segmento com a maior pontuação, ou null.</returns>
        public Segmento? ClassificarSegmento(string titulo, string descricao)
        {
            if (string.IsNullOrWhiteSpace(titulo) && string.IsNullOrWhiteSpace(descricao))
                return null;

            var textoCompleto = $"{titulo} {descricao}".ToLowerInvariant();

            var pontuacoes = new Dictionary<string, int>();

            foreach (var kvp in _termosChave)
            {
                var segmentoId = kvp.Key;
                var termos = kvp.Value;
                int pontuacao = 0;

                foreach (var termo in termos)
                {
                    if (textoCompleto.Contains(termo))
                    {
                        pontuacao++;
                    }
                }

                if (pontuacao > 0)
                {
                    pontuacoes[segmentoId] = pontuacao;
                }
            }

            var segmentoVencedor = pontuacoes.OrderByDescending(p => p.Value).FirstOrDefault();

            if (segmentoVencedor.Key != null)
            {
                return _segmentos[segmentoVencedor.Key];
            }

            return null;
        }
    }
}