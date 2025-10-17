using RadarGov.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class OrgaoDTOs
    {
        public class FiltrosDto
        {
            public List<Orgao> Orgaos { get; set; } = new();
        }
    }

    public class OrgaoDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
