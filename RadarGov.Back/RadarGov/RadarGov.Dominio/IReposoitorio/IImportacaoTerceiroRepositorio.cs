using RadarGov.Dominio.Entidades;
using RadarGov.Infraestrutura.Interfaces;


namespace RadarGov.Dominio.Interfaces
{
    public interface IImportacaoTerceiroRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : BaseImportacaoTerceiro
    {
        Task<TEntity?> ObterPorIdTerceiroAsync(string idTerceiro);
    }

}
