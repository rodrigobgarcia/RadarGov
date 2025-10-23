using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class FonteOrcamentaria : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public FonteOrcamentaria(string idTerceiro, string nome)
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;

        }

        private FonteOrcamentaria()
        {

        }
    }

}
