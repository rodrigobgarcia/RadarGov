using System.Text;
using System.Text.Json;

namespace RadarHub.Integracoes.RadarHub
{
    public class RadarHubClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7203/api";

        private const string HeaderSegredoIntegracao = "X-Integration-Secret";
        private const string ValorSegredoIntegracao = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

        public RadarHubClient()
        {
            var manipulador = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (mensagem, cert, chain, erros) => true
            };

            _httpClient = new HttpClient(manipulador)
            {
                BaseAddress = new Uri(_baseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RadarHubClient/1.0");

            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        /// <summary>
        /// Método interno genérico para enviar requisições com o cabeçalho de segurança.
        /// </summary>
        private async Task<string> EnviarRequisicaoAsync(
            HttpMethod metodo,
            string endpoint,
            object? corpo = null)
        {
            var urlCompleta = $"{_baseUrl}/{endpoint}";
            using var requisicao = new HttpRequestMessage(metodo, urlCompleta);

            requisicao.Headers.Add(HeaderSegredoIntegracao, ValorSegredoIntegracao);

            if (corpo != null && (metodo == HttpMethod.Post || metodo == HttpMethod.Put || metodo == new HttpMethod("PATCH")))
            {
                string jsonBody = JsonSerializer.Serialize(corpo);

                string contentType = (metodo == new HttpMethod("PATCH"))
                    ? "application/json-patch+json"
                    : "application/json";

                requisicao.Content = new StringContent(jsonBody, Encoding.UTF8, contentType);
            }

            var resposta = await _httpClient.SendAsync(requisicao);

            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsStringAsync();
        }

        public Task<string> ObterODataLicitacao(string parametrosOData) =>
            EnviarRequisicaoAsync(HttpMethod.Get, $"Licitacao/OData?{parametrosOData}");

        public Task<string> ObterTodasLicitacoes() =>
            EnviarRequisicaoAsync(HttpMethod.Get, "Licitacao");

        public Task<string> ObterODataSegmento(string parametrosOData) =>
            EnviarRequisicaoAsync(HttpMethod.Get, $"Segmento/OData?{parametrosOData}");

        public Task<string> ObterODataModalidade(string parametrosOData) =>
            EnviarRequisicaoAsync(HttpMethod.Get, $"Modalidade/OData?{parametrosOData}");

        public Task<string> ObterODataOrgao(string parametrosOData) =>
            EnviarRequisicaoAsync(HttpMethod.Get, $"Orgao/OData?{parametrosOData}");

    }
}