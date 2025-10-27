using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class EmpresaDto
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public EmpresaDto(string nome, string cnpj, string email, string senha)
        {
            this.Nome = nome;
            this.Cnpj = cnpj;
            this.Email = email;
            this.Senha = senha;

        }

    }
}
