using RadarGov.Dominio.Entidades;
using RSK.Dominio.IRepositorios;
using RSK.Dominio.Notificacoes.Interfaces;
using RSK.Dominio.Servicos;

namespace RadarGov.Dominio.Servicos
{
    public class EmpresaServico : ServicoCrudBase<Empresa>
    {
        public EmpresaServico(IRepositorioBaseAssincrono<Empresa> repositorio, IServicoMensagem mensagens, ITransacao transacao) : base(repositorio, mensagens, transacao)
        {
        }
    }


}
