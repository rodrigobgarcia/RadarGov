using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadarGov.Dominio.Entidades
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public Empresa? Empresa { get; set; }
        public List<string>? Permissoes { get; set; }

        public Usuario (string nome, string email, string senha, Empresa? empresa, List<string>? permissoes)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Empresa = empresa;
            this.Permissoes = permissoes;
        }
    }
}
