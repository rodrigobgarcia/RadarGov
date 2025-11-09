using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Servicos;
using RSK.API.Controllers;
using RSK.Dominio.Interfaces;
using RSK.Dominio.Notificacoes.Interfaces;

namespace RadarGov.API.Controllers
{
    public class EmpresaController : ApiCrudControllerBase<Empresa>
    {
        public EmpresaController(IServicoConsultaBase<Empresa> servicoConsulta, EmpresaServico servicoCrud, INotificador servicoMensagem) : base(servicoConsulta, servicoCrud, servicoMensagem)
        {
        }
    }
}
