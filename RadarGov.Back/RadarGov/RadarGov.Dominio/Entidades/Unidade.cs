using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Unidade : BaseImportacaoTerceiro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; } 

        public string Nome { get; set; }    

        public int Codigo { get; set; }

        public string CodigoNome { get; set; }

        public Unidade(string idTerceiro, string nome, int codigo, string codigoNome)
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.Codigo = codigo;
            this.CodigoNome = codigoNome;
            this.CriadoEm = DateTime.Now;
            this.UltimaAlteracao = DateTime.Now;
        }

        private Unidade()
        {

        }
    }
}
