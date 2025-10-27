namespace RadarGov.Dominio.Entidades
{
    public class Segmento: EntidadeBase
    {
        public string Id;

        public string Nome;

        public Segmento(string id, string nome)
        {
            Id = id;
            Nome = nome;
            CriadoEm = DateTime.Now;
            UltimaAlteracao = DateTime.Now;
        }

        private Segmento() { }
    }
}
