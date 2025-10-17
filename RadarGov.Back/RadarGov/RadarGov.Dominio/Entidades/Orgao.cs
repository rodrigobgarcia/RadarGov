using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.Entidades
{
    public class Orgao : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public Orgao(string idTerceiro, string nome, string cnpj) 
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.Cnpj = cnpj;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Orgao()
        {

        }
    }
}
