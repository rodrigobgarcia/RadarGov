using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.Entidades
{
    public class Licitacao : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ItemUrl { get; set; }
        public decimal? ValorGlobal { get; set; }
        public bool TemResultado { get; set; }

       
        public string ModalidadeId { get; set; }
        [ForeignKey("ModalidadeId")]
        public Modalidade Modalidade { get; set; }

        
        public string TipoId { get; set; }
        [ForeignKey("TipoId")]
        public Tipo Tipo { get; set; }


        public DateTime? DataPublicacaoPncp { get; set; }
        public DateTime? DataAtualizacaoPncp { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }



        public string Ano { get; set; }
        public string NumeroSequencial { get; set; }
        public string NumeroSequencialCompraAta { get; set; }
        public string NumeroControlePncp { get; set; }



        public string OrgaoId { get; set; }
        [ForeignKey("OrgaoId")]
        public Orgao Orgao { get; set; }

        public string UnidadeId { get; set; }
        [ForeignKey("UnidadeId")]
        public Unidade Unidade { get; set; }

        public string MunicipioId { get; set; }
        [ForeignKey("MunicipioId")]
        public Municipio Municipio { get; set; }

        public string PoderId { get; set; }
        [ForeignKey("PoderId")]
        public Poder Poder { get; set; }

        public string EsferaId { get; set; }
        [ForeignKey("EsferaId")]
        public Esfera Esfera { get; set; }

        public string UfId { get; set; }
        [ForeignKey("UfId")]
        public Ufs Uf { get; set; }

        public string OrgaoSubrogadoId { get; set; }
        public string OrgaoSubrogadoNome { get; set; }

        public string FonteOrcamentariaId { get; set; }
        [ForeignKey("FonteOrcamentariaId")]
        public FonteOrcamentaria FonteOrcamentaria { get; set; }

        public string TipoMargemPreferenciaId { get; set; }
        [ForeignKey("TipoMargemPreferenciaId")]
        public TipoMargemPreferencia TipoMargemPreferencia { get; set; }

        public bool ExigenciaConteudoNacional { get; set; }


        public Segmento Segmento { get; set; }


        
        public Licitacao() { }
    }
}