using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class PoderDTOs
    {
        public class FiltrosDto
        {
            public List<PoderDto> Poderes { get; set; } = new();
        }

        public class PoderDto { 
            
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
