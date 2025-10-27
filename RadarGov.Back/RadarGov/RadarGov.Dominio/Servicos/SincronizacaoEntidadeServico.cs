using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;

namespace RadarGov.Dominio.Servicos
{
    public class SincronizacaoEntidadeServico : ISincronizacaoEntidadeServico
    {
        private readonly IImportacaoTerceiroRepositorio<Orgao> _orgaoRepositorio;
        private readonly IImportacaoTerceiroRepositorio<Municipio> _municipioRepositorio;
        private readonly IImportacaoTerceiroRepositorio<Ufs> _ufsRepositorio;
        private readonly IImportacaoTerceiroRepositorio<FonteOrcamentaria> _fonteOrcamentariaRepositorio;
        private readonly IImportacaoTerceiroRepositorio<Modalidade> _modalidadeRepositorio;
        private readonly IImportacaoTerceiroRepositorio<Tipo> _tipoRepositorio;

        public SincronizacaoEntidadeServico(
            IImportacaoTerceiroRepositorio<Orgao> orgaoRepositorio,
            IImportacaoTerceiroRepositorio<Municipio> municipioRepositorio,
            IImportacaoTerceiroRepositorio<Ufs> ufsRepositorio,
            IImportacaoTerceiroRepositorio<FonteOrcamentaria> fonteOrcamentariaRepositorio,
            IImportacaoTerceiroRepositorio<Modalidade> modalidadeRepositorio,
            IImportacaoTerceiroRepositorio<Tipo> tipoRepositorio)
        {
            _orgaoRepositorio = orgaoRepositorio;
            _municipioRepositorio = municipioRepositorio;
            _ufsRepositorio = ufsRepositorio;
            _fonteOrcamentariaRepositorio = fonteOrcamentariaRepositorio;
            _modalidadeRepositorio = modalidadeRepositorio;
            _tipoRepositorio = tipoRepositorio;
        }

        // --- 1. GARANTIR ORGÃO ---


        public async Task<Orgao> GarantirOrgaoAsync(string idTerceiro, string nome, string cnpj)
        {
            var orgao = await _orgaoRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (orgao == null)
            {
                // Criação (Upsert)
                var novoOrgao = new Orgao(idTerceiro, cnpj, nome);
                await _orgaoRepositorio.AddAsync(novoOrgao);
                return novoOrgao;
            }

            // Atualização
            if (!string.Equals(orgao.Nome, nome, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(orgao.Cnpj, cnpj))
            {
                // NOTA: Assumindo que Orgao tenha a propriedade Cnpj
                orgao.Nome = nome;
                orgao.Cnpj = cnpj;
                orgao.UltimaAlteracao = DateTime.Now;
                await _orgaoRepositorio.UpdateAsync(orgao);
            }

            return orgao;
        }

        // --- 2. GARANTIR MUNICÍPIO ---
        public async Task<Municipio> GarantirMunicipioAsync(string idTerceiro, string nome)
        {
            var municipio = await _municipioRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (municipio == null)
            {
                // Criação
                var novoMunicipio = new Municipio(idTerceiro, nome);
                await _municipioRepositorio.AddAsync(novoMunicipio);
                return novoMunicipio;
            }

            // Atualização
            if (!string.Equals(municipio.Nome, nome, StringComparison.OrdinalIgnoreCase))
            {
                municipio.Nome = nome;
                municipio.UltimaAlteracao = DateTime.Now;
                await _municipioRepositorio.UpdateAsync(municipio);
            }

            return municipio;
        }

        // --- 3. GARANTIR UF (Unidade Federativa) ---
        public async Task<Ufs> GarantirUfsAsync(string idTerceiro)
        {
            var uf = await _ufsRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            // Geralmente, a UF tem apenas um código/sigla (Ex: "SP").
            // O PNCP geralmente fornece o campo 'UF', que usamos como IdTerceiro/Nome.
            if (uf == null)
            {
                // Criação
                // Assumindo que o ID do terceiro (Ex: "SP") é também o nome/sigla
                var novaUf = new Ufs(idTerceiro);
                await _ufsRepositorio.AddAsync(novaUf);
                return novaUf;
            }

            // UFs raramente mudam, então a atualização do nome é geralmente omitida.
            return uf;
        }

        // --- 4. GARANTIR FONTE ORÇAMENTÁRIA ---
        public async Task<FonteOrcamentaria> GarantirFonteOrcamentariaAsync(string idTerceiro, string nome)
        {
            var fonte = await _fonteOrcamentariaRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (fonte == null)
            {
                // Criação
                var novaFonte = new FonteOrcamentaria(idTerceiro, nome);
                await _fonteOrcamentariaRepositorio.AddAsync(novaFonte);
                return novaFonte;
            }

            // Atualização
            if (!string.Equals(fonte.Nome, nome, StringComparison.OrdinalIgnoreCase))
            {
                fonte.Nome = nome;
                fonte.UltimaAlteracao = DateTime.Now;
                await _fonteOrcamentariaRepositorio.UpdateAsync(fonte);
            }

            return fonte;
        }

        // --- 5. GARANTIR MODALIDADE ---
        public async Task<Modalidade> GarantirModalidadeAsync(string idTerceiro, string nome)
        {
            var modalidade = await _modalidadeRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (modalidade == null)
            {
                // Criação
                var novaModalidade = new Modalidade(idTerceiro, nome);
                await _modalidadeRepositorio.AddAsync(novaModalidade);
                return novaModalidade;
            }

            // Atualização
            if (!string.Equals(modalidade.Nome, nome, StringComparison.OrdinalIgnoreCase))
            {
                modalidade.Nome = nome;
                modalidade.UltimaAlteracao = DateTime.Now;
                await _modalidadeRepositorio.UpdateAsync(modalidade);
            }

            return modalidade;
        }

        // --- 6. GARANTIR TIPO ---
        public async Task<Tipo> GarantirTipoAsync(string idTerceiro, string nome)
        {
            var tipo = await _tipoRepositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (tipo == null)
            {
                // Criação
                var novoTipo = new Tipo(idTerceiro, nome);
                await _tipoRepositorio.AddAsync(novoTipo);
                return novoTipo;
            }

            // Atualização
            if (!string.Equals(tipo.Nome, nome, StringComparison.OrdinalIgnoreCase))
            {
                tipo.Nome = nome;
                tipo.UltimaAlteracao = DateTime.Now;
                await _tipoRepositorio.UpdateAsync(tipo);
            }

            return tipo;
        }
    }
}
