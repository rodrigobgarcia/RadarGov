using RadarGov.Dominio.Entidades;
using RadarGov.Infraestrutura.Interfaces;


namespace RadarGov.Dominio.Interfaces
{
    public interface IModalidadeRepositorio : IBaseRepositorio<Modalidade>
    {
        Task<Modalidade?> ObterPorIdTerceiroAsync(string idTerceiro);
    }
}
