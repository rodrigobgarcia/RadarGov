using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.Entidades
{
    public class Licitacao
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string OrgaoResponsavel { get; set; }

        public string PortalFonte { get; set; }

        public string Url { get; set; }

        public DateOnly DataPublicacao { get; set; }

        public DateOnly DataLimiteProposta {  get; set; }

        public string Tipo {  get; set; }

        public string Status { get; set; }

        public string Descricao { get; set; }

        public Segmento Segmento { get; set; } 

        public Regiao Regiao { get; set; }


        public Licitacao(int id, string titulo, string orgaoResponsavel, string portalFonte, string url, DateOnly dataPublicacao, DateOnly dataLimiteProposta,string tipo, string status, string descricao, Segmento segmento, Regiao regiao)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.OrgaoResponsavel = orgaoResponsavel;
            this.PortalFonte = portalFonte;
            this.Url = url;
            this.DataPublicacao = dataPublicacao;
            this.DataLimiteProposta = dataLimiteProposta;
            this.Tipo = tipo;
            this.Status = status;
            this.Descricao = descricao;
            this.Segmento = segmento;
            this.Regiao = regiao;
        }
    }
}
