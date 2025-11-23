using RSK.Dominio.Entidades;

namespace RadarGov.Dominio.DTOs
{
    public class ModalidadeDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
    }

    public class OrgaoDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
        //public string Sigla { get; set; }
    }

    public class MunicipioDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
        //public string UF { get; set; }
    }

    public class UfDto : EntidadeBaseImportacaoTerceiro
    {
    }

    public class TipoMargemPreferenciaDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
    }

    public class PoderDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
    }

    public class UnidadeDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
        public string? Codigo { get; set; }
        public string? CodigoNome { get; set; }
    }

    public class TipoDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
    }


    public class SegmentoDto : EntidadeBase
    {
        public string Nome { get; set; }
    }

}