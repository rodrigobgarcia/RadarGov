using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class EsferaDTOs
    {
        public class FiltrosDtp
        {
            public List<EsferaDto> Esferas { get; set; } = new();
        }

        public class EsferaDto
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}
