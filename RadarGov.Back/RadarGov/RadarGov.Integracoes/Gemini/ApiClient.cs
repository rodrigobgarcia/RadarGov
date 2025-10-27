using System.Net.Http.Json;


namespace RadarGov.Integracoes.Gemini
{
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BASE_URL = "https://generativelanguage.googleapis.com/v1beta/models/";

        public GeminiApiClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "A chave de API não pode ser nula ou vazia.");
            }

            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string?> GenerateContentAsync<T>(string modelName, T requestBody) where T : class
        {
            string requestUrl = $"{BASE_URL}{modelName}:generateContent?key={_apiKey}";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"[ERRO GEMINI] Status: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    Console.WriteLine($"[ERRO GEMINI] Conteúdo da Resposta (Detalhe do Erro da API): {errorContent}");

                    return null;
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO GEMINI] Exceção na chamada: {ex.Message}");
                return null;
            }
        }
    }
}