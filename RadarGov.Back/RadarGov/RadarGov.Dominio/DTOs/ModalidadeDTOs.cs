namespace RadarGov.Dominio.DTOs
{
    public class ModalidadeDTOs
    {
        // DTO para mapear apenas modalidades do JSON
        public class FiltrosDto
        {
            public List<ModalidadeDto> Modalidades { get; set; } = new();
        }

        public class ModalidadeDto
        {
            public string Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
