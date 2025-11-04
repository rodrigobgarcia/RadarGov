namespace RadarGov.Infraestrutura.Multitenancy
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string ConnectionString { get; set; }
        public bool Ativo { get; set; }
    }

}
