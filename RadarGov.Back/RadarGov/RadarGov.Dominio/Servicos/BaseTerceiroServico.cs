using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.Notificacoes.Entidades;

namespace RadarGov.Dominio.Servicos
{
    public abstract class BaseTerceiroServico<TEntity> where TEntity : BaseImportacaoTerceiro
    {
        protected readonly IImportacaoTerceiroRepositorio<TEntity> Repositorio;
        protected readonly MensagemServico Mensagens;

        public BaseTerceiroServico(
            IImportacaoTerceiroRepositorio<TEntity> repositorio,
            MensagemServico mensagens)
        {
            Repositorio = repositorio;
            Mensagens = mensagens;
        }

        public async Task<TEntity?> ObterPorIdTerceiroAsync(string idTerceiro)
        {
            var entidade = await Repositorio.ObterPorIdTerceiroAsync(idTerceiro);

            if (entidade == null)
            {

                var nomeEntidade = typeof(TEntity).Name;
                Mensagens.Adicionar(
                    $"{nomeEntidade} com ID de Terceiro '{idTerceiro}' não encontrado. A sincronização deste dado mestre deve ser executada antes.",
                    TipoMensagem.Erro);

                return null;
            }

            return entidade;
        }
    }
}