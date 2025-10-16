using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Esfera 
    {
        public string Id { get; set;}

        public string IdTerceiro { get; set;}

        public string Nome { get; set;}

       public DateTime CriadoEM { get; set;}

       public DateTime UltimaAteracao { get; set;}


        public Esfera(string idTerceiro, string nome)
        {
            this.IdTerceiro = idTerceiro;
            this.Nome = nome;
            this.CriadoEM = DateTime.Now;
            this.UltimaAteracao = DateTime.Now;
        }

        private Esfera() { 
        }
    }
}
