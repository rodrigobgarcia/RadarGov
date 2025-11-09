using RSK.Dominio.Entidades;

namespace RadarGov.Dominio.Entidades
{
    public class Empresa : EntidadeBase
    {
        public string Nome { get; set; }
        //public string? TenantId { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }

        // ==========================================================
        // PREFERÊNCIAS DE LICITAÇÃO (PARA FILTRAGEM E RECOMENDAÇÃO)
        // ==========================================================

        /// <summary>
        /// Lista de IDs de Órgãos (IdTerceiro) de interesse da empresa.
        /// </summary>
        public List<string> OrgaosIdTerceiroPreferidos { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Municípios (IdTerceiro) de interesse da empresa.
        /// </summary>
        public List<string> MunicipiosIdTerceiroPreferidos { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Modalidades (IdTerceiro) de interesse da empresa.
        /// </summary>
        public List<string> ModalidadesIdTerceiroPreferidas { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Tipos (IdTerceiro) de interesse da empresa.
        /// </summary>
        public List<string> TiposIdTerceiroPreferidos { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de UFs (IdTerceiro) de interesse da empresa.
        /// </summary>
        public List<string> UfsIdTerceiroPreferidas { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Fontes Orçamentárias (IdTerceiro) de interesse.
        /// </summary>
        public List<string> FontesOrcamentariasIdTerceiroPreferidas { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Tipos de Margem de Preferência (IdTerceiro) de interesse.
        /// </summary>
        public List<string> TiposMargemPreferenciaIdTerceirosPreferidos { get; set; } = new List<string>();

        /// <summary>
        /// Lista de IDs de Poderes (IdTerceiro) de interesse.
        /// </summary>
        public List<string> PoderesIdTerceiroPreferidos { get; set; } = new List<string>();

        /// <summary>
        /// Indica se a empresa tem preferência ou requisito por licitações com exigência de Conteúdo Nacional.
        /// </summary>
        public bool PrefereExigenciaConteudoNacional { get; set; } = false;

        // ==========================================================
        // CONSTRUTORES
        // ==========================================================

        public Empresa(string nome, string tenantId, string cnpj, string email)
        {
            this.Nome = nome;
            this.Email = email;
            this.Cnpj = cnpj;
        }

        private Empresa()
        {
            // Construtor privado para ORM
        }

    }
}