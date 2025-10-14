using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.Pnc;
using System.Text.Json;
using static RadarGov.Dominio.DTOs.ModalidadeDTOs;

namespace RadarGov.Dominio.Servicos
{
    public class ModalidadeServico
    {
        private readonly IModalidadeRepositorio _modalidadeRepositorio;
        private readonly Pncp _pncp;

        public ModalidadeServico(IModalidadeRepositorio modalidadeRepositorio)
        {
            _modalidadeRepositorio = modalidadeRepositorio;
            _pncp = new Pncp();
        }

        public async Task<bool> ImportarModalidadesAsync()
        {
            try
            {
                var json = await _pncp.GetFiltros();

                var filtros = JsonSerializer.Deserialize<FiltrosDto>(json);

                if (filtros?.Modalidades != null)
                {
                    foreach (var item in filtros.Modalidades)
                    {
                        // 3. Verificar se já existe pelo IdTerceiro
                        var existente = await _modalidadeRepositorio.ObterPorIdTerceiroAsync(item.Id);
                        if (existente == null)
                        {
                            var nova = new Modalidade(item.Id, item.Nome);
                            await _modalidadeRepositorio.AddAsync(nova);
                        }
                    }

                    await _modalidadeRepositorio.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
