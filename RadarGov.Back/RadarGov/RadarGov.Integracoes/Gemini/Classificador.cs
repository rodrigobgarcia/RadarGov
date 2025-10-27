using System.Text.Json;


namespace RadarGov.Integracoes.Gemini
{

    public class SegmentoClassifierService
    {
        private readonly GeminiApiClient _geminiApiClient;
        private const string MODEL_NAME = "gemini-2.5-flash";

        public SegmentoClassifierService(GeminiApiClient geminiApiClient)
        {
            _geminiApiClient = geminiApiClient;
        }


        public async Task<string?> ClassificarSegmentoAsync(string titulo, string descricao)
        {

            string prompt = $@"
                Você é um especialista em classificação de licitações governamentais.
                Sua tarefa é analisar o Título e a Descrição e classificar o item em um único,
                nome de segmento **curto, canônico e em maiúsculas**.
                
                Retorne APENAS um objeto JSON com o seguinte formato, SEM QUALQUER TEXTO ADICIONAL:
                {{ ""segmento_nome"": ""[Seu Segmento Aqui]"" }}

                Título: {titulo}
                Descrição: {descricao}
            ";

            var requestBody = new
            {
                contents = new[]
                {
                    new ContentRequest
                    {
                        Parts = { new PartRequest { Text = prompt } }
                    }
                },
                generationConfig = new ConfigRequest
                {
                    Temperature = 0.1,
                    ResponseMimeType = "application/json",
                    ResponseSchema = new SchemaRequest
                    {
                        Properties = new Dictionary<string, PropertySchema>
                        {
                            { "segmento_nome", new PropertySchema { Description = "O nome canônico e em maiúsculas do segmento da licitação." } }
                        }
                    }
                }
            };

            string? rawJsonResponse = await _geminiApiClient.GenerateContentAsync(MODEL_NAME, requestBody);

            if (string.IsNullOrEmpty(rawJsonResponse))
            {
                return null;
            }
            try
            {
                var apiResponse = JsonSerializer.Deserialize<GenerateContentResponse>(rawJsonResponse);

    
                string? generatedJsonText = apiResponse?.Candidates?
                    .FirstOrDefault()?
                    .Content?
                    .Parts?
                    .FirstOrDefault()?
                    .Text;

                if (string.IsNullOrEmpty(generatedJsonText))
                {
                    return null;
                }


                using (JsonDocument document = JsonDocument.Parse(generatedJsonText.Trim()))
                {
                    if (document.RootElement.TryGetProperty("segmento_nome", out JsonElement nomeElement) && nomeElement.ValueKind == JsonValueKind.String)
                    {
                        return nomeElement.GetString()?.ToUpperInvariant().Trim();
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Erro ao analisar JSON de resposta do Gemini: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na extração do segmento: {ex.Message}");
                return null;
            }

            return null;
        }

    }
}