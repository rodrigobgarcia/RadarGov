using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Regiao
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Regiao(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
