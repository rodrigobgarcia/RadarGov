using System.Text.Json.Serialization;

// DTO para o Corpo da Requisição de Conteúdo
public class ContentRequest
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = "user";

    [JsonPropertyName("parts")]
    public List<PartRequest> Parts { get; set; } = new List<PartRequest>();
}

public class PartRequest
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class ConfigRequest
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; } = 0.1;

    [JsonPropertyName("responseMimeType")]
    public string ResponseMimeType { get; set; } = "application/json";

    [JsonPropertyName("responseSchema")]
    public SchemaRequest? ResponseSchema { get; set; }
}

public class SchemaRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "OBJECT";

    [JsonPropertyName("properties")]
    public Dictionary<string, PropertySchema> Properties { get; set; } = new Dictionary<string, PropertySchema>();
}

public class PropertySchema
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "string";

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

// DTO para a Resposta (simplificado)
public class ResponseCandidate
{
    [JsonPropertyName("content")]
    public ContentRequest? Content { get; set; }
}

public class GenerateContentResponse
{
    [JsonPropertyName("candidates")]
    public List<ResponseCandidate>? Candidates { get; set; }
}