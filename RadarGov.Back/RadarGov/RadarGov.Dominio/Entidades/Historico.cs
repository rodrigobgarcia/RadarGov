using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Historico
    {
        public int Id {  get; set; }

        public DateOnly DataAcesso {  get; set; }

        public string Descricao { get; set; }

        public Empresa Empresa { get; set; }

        public Licitacao LicitacaoVisualisada { get; set; }

        public Historico(int id, DateOnly dataAcesso, string descricao, Empresa empresa, Licitacao licitacaoVisualisada) { 
            this.Id = id;
            this.DataAcesso= dataAcesso;
            this.Descricao = descricao;
            this.Empresa = empresa;
            this.LicitacaoVisualisada = licitacaoVisualisada;
        }
    }
}
