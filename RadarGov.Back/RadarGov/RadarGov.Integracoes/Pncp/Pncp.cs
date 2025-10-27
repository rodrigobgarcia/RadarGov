﻿using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace RadarGov.Integracoes.Pnc
{
    public class Pncp
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://pncp.gov.br";

        public Pncp()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RadarGovBot/1.0");
        }

        /// <summary>
        /// Método interno para fazer requisições GET, tratando gzip automaticamente.
        /// </summary>
        private async Task<string> GetAsync(string endpoint)
        {
            var resposta = await _httpClient.GetAsync(endpoint);
            resposta.EnsureSuccessStatusCode();

            var encodings = resposta.Content.Headers.ContentEncoding;
            if (encodings.Contains("gzip"))
            {
                using var compressedStream = await resposta.Content.ReadAsStreamAsync();
                using var decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress);
                using var reader = new StreamReader(decompressedStream, Encoding.UTF8);
                return await reader.ReadToEndAsync();
            }

            return await resposta.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Retorna todos os filtros disponíveis da API do PNCP para o tipo de documento "edital".
        /// </summary>
        public async Task<string> GetFiltros()
        {
            try
            {
                return await GetAsync("/api/search/filters?tipos_documento=edital");
            }
            catch (Exception ex)
            {
                return $"Erro ao obter filtros: {ex.Message}";
            }
        }

        /// <summary>
        /// Retorna todas as licitações que estão com status "recebendo_proposta".
        /// Faz a paginação automaticamente até carregar todos os resultados.
        /// </summary>
        public async Task<List<LicitacaoResponse>> GetTodasLicitacoesRecebendoProposta(int tamanhoPagina = 1000)
        {
            var todas = new List<LicitacaoResponse>();
            int pagina = 1;
            int totalPaginas = 1;

            do
            {
                string parametros = $"?tipos_documento=edital&ordenacao=-data&pagina={pagina}&tam_pagina={tamanhoPagina}&status=recebendo_proposta";
                string json = await GetAsync($"/api/search/{parametros}");

                var resposta = JsonSerializer.Deserialize<LicitacaoDto>(json);

                if (resposta?.Items != null)
                {
                    todas.AddRange(resposta.Items);
                }

                long total = resposta.Total;
                totalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina);

                Console.WriteLine($"Página {pagina}/{totalPaginas} carregada ({todas.Count} itens até agora)");

                pagina++;

                await Task.Delay(500);

            } while (pagina <= totalPaginas);

            return todas;
        }
    }
}
