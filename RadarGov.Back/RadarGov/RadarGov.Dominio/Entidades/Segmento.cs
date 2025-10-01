using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Segmento
    {
        public int Id;

        public string Nome;

        public Segmento(int id, string nome) {
            this.Id = id;
            this.Nome = nome;
        }
    }
}
