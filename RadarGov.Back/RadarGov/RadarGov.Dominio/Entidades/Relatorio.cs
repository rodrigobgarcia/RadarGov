using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Relatorio
    {
        public int Id;

        public DateTime DataGeracao {  get; set; }

        public string Tipo { get; set; }

        public string Formato { get; set; }

        public Empresa Empresa { get; set; }

        public List<Licitacao> LicitacoesIncluidas { get; set; } 


     public Relatorio(int id, DateTime dataGeracao, string tipo, string formato, Empresa empresa, List<Licitacao> licitacoesIncluidas)
        {
            this.Id = id;
            this.DataGeracao = dataGeracao;
            this.Tipo = tipo;
            this.Formato = formato;
            this.Empresa = empresa;
            this.LicitacoesIncluidas = licitacoesIncluidas;

        }
    }
}
