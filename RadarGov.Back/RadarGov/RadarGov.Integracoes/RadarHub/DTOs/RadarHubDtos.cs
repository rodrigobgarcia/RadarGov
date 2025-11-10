using RSK.Dominio.Entidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace RadarGov.Dominio.DTOs
{
    public class ModalidadeDto: EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
    }

    public class OrgaoDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }

    public class MunicipioDto : EntidadeBaseImportacaoTerceiro
    {
        public string Nome { get; set; }
        public string UF { get; set; }
    }

    public class UfDto : EntidadeBaseImportacaoTerceiro
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}