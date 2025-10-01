namespace RadarGov.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public Empresa Empresa { get; set; }
        public List<string> Permissoes { get; set; }
    }
}
