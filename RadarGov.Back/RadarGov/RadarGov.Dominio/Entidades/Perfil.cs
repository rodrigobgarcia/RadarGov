namespace RadarGov.Dominio.Entidades
{
    public class Perfil
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<string>? Permissoes { get; set; }
    }
}
