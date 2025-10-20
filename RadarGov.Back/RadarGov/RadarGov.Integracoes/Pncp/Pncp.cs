using System.IO.Compression;
using System.Text;

namespace RadarGov.Integracoes.Pnc
{
    public class Pncp
    {
        private readonly string _urlBase;

        public Pncp()
        {
            _urlBase = "https://pncp.gov.br";
        }

        public async Task<string> GetFiltros()
        {
            using var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_urlBase);

            try
            {
                // Faz a requisição para a API
                var resposta = await cliente.GetAsync("/api/search/filters?tipos_documento=edital&");

                resposta.EnsureSuccessStatusCode();

                // Verifique se o conteúdo está comprimido com GZIP
                var encoding = resposta.Content.Headers.ContentEncoding.ToString();

                if (encoding.Contains("gzip"))
                {
                    using var compressedStream = await resposta.Content.ReadAsStreamAsync();
                    using var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                    using var reader = new StreamReader(decompressedStream, Encoding.UTF8);

                    var corpoResposta = await reader.ReadToEndAsync();

                    return corpoResposta;
                }
                else
                {
                    return await resposta.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Erro ao fazer a requisição: {ex.Message}";
            }
        }
    }
}
