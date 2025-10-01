using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Plano
    {
        public string Nome { get; set; }

        public int LimiteCategorias { get; set; }

        public bool HistoricoCompleto { get; set; }

        public bool AlertasEmTempoReal {  get; set; }

        public bool RelatoriosAvancados { get; set; }

        public bool Chatbot { get; set; }

        public Plano(string nome, int limiteCategorias, bool historicoCompleto, bool alertasEmTempoReal, bool relatoriosAvancados, bool chatbot)
        {
            this.Nome = nome;
            this.LimiteCategorias = limiteCategorias;
            this.HistoricoCompleto = historicoCompleto;
            this.AlertasEmTempoReal = alertasEmTempoReal;
            this.RelatoriosAvancados = relatoriosAvancados;
            this.Chatbot = chatbot;
        }
        
    }
}
