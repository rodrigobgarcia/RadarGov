using System;
using System.IO;
using System.Net.Http;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

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

                // Garante que a resposta tenha sido bem-sucedida (status 2xx)
                resposta.EnsureSuccessStatusCode();

                // Verifique se o conteúdo está comprimido com GZIP
                var encoding = resposta.Content.Headers.ContentEncoding.ToString();

                if (encoding.Contains("gzip"))
                {
                    // Se a resposta for comprimida, descompacte o conteúdo
                    using var compressedStream = await resposta.Content.ReadAsStreamAsync();
                    using var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                    using var reader = new StreamReader(decompressedStream, Encoding.UTF8);

                    // Leia o conteúdo descomprimido
                    var corpoResposta = await reader.ReadToEndAsync();

                    return corpoResposta;
                }
                else
                {
                    // Se o conteúdo não estiver comprimido, apenas leia a resposta
                    return await resposta.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                // Capture e retorne o erro se houver falha
                return $"Erro ao fazer a requisição: {ex.Message}";
            }
        }
    }
}
