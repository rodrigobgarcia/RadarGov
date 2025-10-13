namespace RadarGov.Integracoes.Pnc
{
    public class Pncp
    {
        private string _urlBase { get; set; }
        public Pncp()
        {
            _urlBase = "https://pncp.gov.br/api";
        }

        //public async Task GetFiltros()
        //{
        //   using var cliente = new HttpClient();
        //   cliente.BaseAddress = new Uri(_urlBase);
        //   var resposta = await cliente.GetAsync("/search/filters?tipos_documento=edital");
        //}

        public async Task<string> GetFiltros()
        {
            using var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_urlBase);
            var resposta = await cliente.GetAsync("/search/filters?tipos_documento=edital");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsStringAsync();
        }


    }
}
