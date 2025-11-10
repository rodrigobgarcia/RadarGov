using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Entidades;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RadarGov.Integracoes.RadarHub
{
    public class RadarHubIntegracaoServico : IRadarHubIntegracaoServico
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<RadarHubIntegracaoServico> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public RadarHubIntegracaoServico(
            HttpClient httpClient, 
            IConfiguration configuration,
            ILogger<RadarHubIntegracaoServico> logger)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["RadarHubApi:BaseUrl"];
            _logger = logger;
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Carrega headers opcionais configurados em appsettings.json em RadarHubApi:Headers
            // Exemplo de configuração:
            // "RadarHubApi": {
            //   "BaseUrl": "https://api.radarhub.local",
            //   "Headers": {
            //     "X-Api-Key": "valor-aqui",
            //     "X-Tenant": "tenant-id"
            //   }
            // }
            var headersSection = configuration.GetSection("RadarHubApi:Headers");
            if (headersSection.Exists())
            {
                foreach (var h in headersSection.GetChildren())
                {
                    var headerName = h.Key;
                    var headerValue = h.Value;
                    if (!string.IsNullOrEmpty(headerName) && !string.IsNullOrEmpty(headerValue))
                    {
                        // TryAddWithoutValidation permite adicionar valores sem validação estrita do formato
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(headerName, headerValue);
                    }
                }
            }
        }

        public async Task<IEnumerable<ModalidadeDto>> ObterModalidades()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/modalidade");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Resposta do RadarHub (Modalidades): {Content}", content);
            
            var result = JsonSerializer.Deserialize<IEnumerable<ModalidadeDto>>(content, _jsonOptions);
            if (result == null || !result.Any())
            {
                _logger.LogWarning("Nenhuma modalidade encontrada ou erro na deserialização");
            }
            return result ?? Enumerable.Empty<ModalidadeDto>();
        }

        public async Task<IEnumerable<OrgaoDto>> ObterOrgaos()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/orgao");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Resposta do RadarHub (Órgãos): {Content}", content);
            
            var result = JsonSerializer.Deserialize<IEnumerable<OrgaoDto>>(content, _jsonOptions);
            if (result == null || !result.Any())
            {
                _logger.LogWarning("Nenhum órgão encontrado ou erro na deserialização");
            }
            return result ?? Enumerable.Empty<OrgaoDto>();
        }

        public async Task<IEnumerable<MunicipioDto>> ObterMunicipios(string uf = null)
        {
            var url = $"{_baseUrl}/api/municipio";
            if (!string.IsNullOrEmpty(uf))
            {
                url += $"?uf={uf}";
            }
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Resposta do RadarHub (Municípios): {Content}", content);
            
            var result = JsonSerializer.Deserialize<IEnumerable<MunicipioDto>>(content, _jsonOptions);
            if (result == null || !result.Any())
            {
                _logger.LogWarning("Nenhum município encontrado ou erro na deserialização");
            }
            return result ?? Enumerable.Empty<MunicipioDto>();
        }

        public async Task<IEnumerable<UfDto>> ObterUfs()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/ufs");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Resposta do RadarHub (UFs): {Content}", content);
            
            var result = JsonSerializer.Deserialize<IEnumerable<UfDto>>(content, _jsonOptions);
            if (result == null || !result.Any())
            {
                _logger.LogWarning("Nenhuma UF encontrada ou erro na deserialização");
            }
            return result ?? Enumerable.Empty<UfDto>();
        }

        public async Task<IEnumerable<RadarGov.Integracoes.Pncp.LicitacaoResponse>> ObterLicitacoesRecomendadas(long empresaId)
        {
            // O RadarHub é a fonte de todas as licitações; aqui buscamos todas e retornamos o DTO de integração.
            var url = $"{_baseUrl}/api/licitacao";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Resposta do RadarHub (Todas as licitações): {Content}", content);

            try
            {
                // Tenta primeiro desserializar para o envelope Pncp (com Items)
                var dto = JsonSerializer.Deserialize<RadarGov.Integracoes.Pncp.LicitacaoDto>(content, _jsonOptions);
                IEnumerable<RadarGov.Integracoes.Pncp.LicitacaoResponse>? items = null;
                if (dto != null && dto.Items != null)
                {
                    items = dto.Items;
                }
                else
                {
                    // Se não for um envelope, tenta desserializar diretamente uma coleção
                    items = JsonSerializer.Deserialize<IEnumerable<RadarGov.Integracoes.Pncp.LicitacaoResponse>>(content, _jsonOptions);
                }

                return items ?? Enumerable.Empty<RadarGov.Integracoes.Pncp.LicitacaoResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao desserializar licitações do RadarHub");
                return Enumerable.Empty<RadarGov.Integracoes.Pncp.LicitacaoResponse>();
            }
        }
    }
}