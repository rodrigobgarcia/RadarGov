using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class UnidadeDTOs
    {
        public class FiltrosDto
        {
            public List<UnidadeDTO> Unidades { get; set; } = new();
        }

        public class UnidadeDTO
        {
            public string Id { get; set; }

            public string Nome { get; set; }    
        }
    }
}
