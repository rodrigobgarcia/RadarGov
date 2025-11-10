using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.RadarHub;

namespace RadarGov.Infraestrutura.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            // Outros serviços...

            // RadarHub Integration
            services.AddHttpClient<IRadarHubIntegracaoServico, RadarHubIntegracaoServico>();

            return services;
        }
    }
}