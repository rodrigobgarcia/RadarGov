using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadarGov.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Empresa? Empresa { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime? DataDesativacao { get; set; } = null;

        public Usuario (string nome, string email, string senha, Empresa? empresa)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Empresa = empresa;
        }

        private Usuario()
        {

        }
    }
}
