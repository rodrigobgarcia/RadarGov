using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadarGov.Dominio.Entidades
{
    public class Tipo : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public string Id { get; set; }
        public string Nome { get; set; }    

        public Tipo(string idTerceiro, string nome)
        {
            this.Id = idTerceiro;
            this.Nome = nome;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Tipo() 
        {
        }
    }
}
