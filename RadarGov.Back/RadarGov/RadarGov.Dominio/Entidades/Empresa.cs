using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Empresa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TenantId { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public List<string> SegmentoInteresse { get; set; }
        public List<string> RegioesAtuacao { get; set; }

        public Empresa(string nome, string tenantId, string cnpj, string email, string senha, DateTime criadoEm, List<string> segmentoInteresse, List<string> RegioesAtuacao)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Cnpj = cnpj;
            this.TenantId = tenantId;
            this.CriadoEm = criadoEm;
            this.SegmentoInteresse = segmentoInteresse;
            this.RegioesAtuacao = RegioesAtuacao;
        }

        private Empresa()
        {

        }

    }
}
