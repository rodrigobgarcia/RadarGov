using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class TipoMargemPreferencia : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string  Id { get; set; }

        public string Nome { get; set; }

        public TipoMargemPreferencia(string id, string name)
        {
            this.Id = id;
            this.Nome = name;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private TipoMargemPreferencia()
        {

        }
    }
}
