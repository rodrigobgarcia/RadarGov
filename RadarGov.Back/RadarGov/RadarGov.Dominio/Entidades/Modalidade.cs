using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.Entidades
{
    public class Modalidade : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Nome { get; set; }

        public Modalidade(string idTerceiro, string nome)
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Modalidade()
        {
        }

    }
}
