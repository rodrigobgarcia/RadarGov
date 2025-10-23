using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.Entidades
{
    public class Ufs: BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public Ufs(string idTerceiro)
        {
            this.IdTerceiro = idTerceiro;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Ufs()
        {
        }
    }
}
