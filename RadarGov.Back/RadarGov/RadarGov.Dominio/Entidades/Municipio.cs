using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.Entidades
{
    public class Municipio : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Nome { get; set; }

        public Municipio(string idTerceiro, string nome)
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Municipio()
        {
        }

    }
}
