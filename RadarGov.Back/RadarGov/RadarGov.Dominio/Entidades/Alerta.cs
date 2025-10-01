using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Alerta
    {
        public int Id { get; set; }

        public DateOnly DataEnvio { get; set; }

        public string Tipo  { get; set; }

        public Licitacao Licitacao { get; set; }

        public Empresa Empresa { get; set; }

        public Alerta(int id, DateOnly dataEnvio, string tipo, Licitacao licitacao, Empresa empresa)
        {
            this.Id = id;
            this.DataEnvio = dataEnvio;
            this.Tipo = tipo;
            this.Licitacao = licitacao;
            this.Empresa = empresa;
        }
    }
}
