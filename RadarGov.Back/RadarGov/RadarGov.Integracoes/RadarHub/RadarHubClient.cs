using RadarGov.Dominio.DTOs;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.RadarHub.DTOs;
using RSK.Dominio.Autorizacao.Entidades;
using RSK.Dominio.Autorizacao.Helpers;
using RSK.Dominio.IRepositorios;
using System.Text;
using System.Text.Json;

public class RadarHubClient : IRadarHubIntegracaoServico
{
    private readonly HttpClient _httpClient;
    private readonly IRepositorioBaseAssincrono<AplicacaoIntegrada> _repositorioAplicacao;

    private const string HeaderSegredoIntegracao = "X-Integration-Secret";

    private string? _baseUrl;
    private string? _valorSegredoIntegracao;
    private bool _configuracaoCarregada;

    public RadarHubClient(
        HttpClient httpClient,
        IRepositorioBaseAssincrono<AplicacaoIntegrada> repoAplicacao)
    {
        _httpClient = httpClient;
        _repositorioAplicacao = repoAplicacao;
    }

    private async Task CarregarConfiguracoesAsync()
    {
        if (_configuracaoCarregada)
            return;

        var aplicacao = await AplicacaoIntegradaHelper.ObterPorIdAsync(_repositorioAplicacao, 1);

        if (aplicacao == null)
            throw new InvalidOperationException("Aplicação integrada não encontrada.");

        _baseUrl = aplicacao.Url
            ?? throw new InvalidOperationException("A aplicação não possui URL configurada.");

        _valorSegredoIntegracao = aplicacao.IntegrationSecretHash
            ?? throw new InvalidOperationException("A aplicação não possui chave de integração configurada.");

        _httpClient.BaseAddress = new Uri(_baseUrl.TrimEnd('/'));

        _configuracaoCarregada = true;
    }

    private async Task<string> EnviarRequisicaoAsync(HttpMethod metodo, string endpoint, object? corpo = null)
    {
        await CarregarConfiguracoesAsync();

        using var requisicao = new HttpRequestMessage(metodo, endpoint);

        requisicao.Headers.Add(HeaderSegredoIntegracao, _valorSegredoIntegracao!);

        if (corpo != null && (metodo == HttpMethod.Post || metodo == HttpMethod.Put))
        {
            string jsonBody = JsonSerializer.Serialize(corpo);
            requisicao.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }

        try
        {
            var resposta = await _httpClient.SendAsync(requisicao);
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Erro ao comunicar com RadarHub ({endpoint}): {ex.Message}", ex);
        }
    }


    public Task<string> ObterODataLicitacaoAsync(string parametrosOData)
            => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Licitacao?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataModalidadeAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Modalidade?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataSegmentoAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Segmento?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataOrgaoAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Orgao?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataMunicipiosAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Municipio?$count=true&{parametrosOData?.TrimStart('?')}");
    public Task<string> ObterODataUfAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Ufs?$count=true&{parametrosOData?.TrimStart('?')}");
    public Task<string> ObterODataTipoMargemPreferenciaAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/TipoMargemPreferencia?$count=true&{parametrosOData?.TrimStart('?')}");
    public Task<string> ObterODataPoderAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Poder?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataUnidadeAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Unidade?$count=true&{parametrosOData?.TrimStart('?')}");

    public Task<string> ObterODataTipoAsync(string parametrosOData)
        => EnviarRequisicaoAsync(HttpMethod.Get, $"api/Tipo?$count=true&{parametrosOData?.TrimStart('?')}");

    public async Task<ODataResult<ModalidadeDto>> ObterModalidadesAsync(string parametrosOData)
    {
        var json = await ObterODataModalidadeAsync(parametrosOData ?? string.Empty);

        var odata = JsonSerializer.Deserialize<ODataResult<ModalidadeDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<ModalidadeDto>();
    }

    public async Task<ODataResult<OrgaoDto>> ObterOrgaosAsync(string parametrosOData)
    {
        var json = await ObterODataOrgaoAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<OrgaoDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<OrgaoDto>();
    }

    public async Task<ODataResult<MunicipioDto>> ObterMunicipiosAsync(string parametrosOData)
    {
        var json = await ObterODataMunicipiosAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<MunicipioDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<MunicipioDto>();
    }

    public async Task<ODataResult<UfDto>> ObterUfsAsync(string parametrosOData)
    {
        var json = await ObterODataUfAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<UfDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<UfDto>();
    }

    public async Task<ODataResult<TipoMargemPreferenciaDto>> ObterTipoMargemPreferenciaAsync(string parametrosOData)
    {
        var json = await ObterODataTipoMargemPreferenciaAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<TipoMargemPreferenciaDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<TipoMargemPreferenciaDto>();
    }

    public async Task<ODataResult<PoderDto>> ObterPoderAsync(string parametrosOData)
    {
        var json = await ObterODataPoderAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<PoderDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<PoderDto>();
    }
    public async Task<ODataResult<UnidadeDto>> ObterUnidadeAsync(string parametrosOData)
    {
        var json = await ObterODataUnidadeAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<UnidadeDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<UnidadeDto>();
    }

    public async Task<ODataResult<TipoDto>> ObterTipoAsync(string parametrosOData)
    {
        var json = await ObterODataTipoAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<TipoDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<TipoDto>();
    }

    public async Task<ODataResult<SegmentoDto>> ObterSegmentoAsync(string parametrosOData)
    {
        var json = await ObterODataSegmentoAsync(parametrosOData);

        var odata = JsonSerializer.Deserialize<ODataResult<SegmentoDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return odata ?? new ODataResult<SegmentoDto>();
    }

}

