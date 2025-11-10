using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RadarGov.API.Extensoes
{
    // IDocumentFilter atua no documento final do Swagger
    public class SwaggerIgnoreAbstractControllerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // 1. Identificar as rotas (caminhos) a serem removidas
            var pathsToRemove = new List<string>();

            foreach (var apiDescription in context.ApiDescriptions)
            {
                // Tenta obter o descritor de ação para acessar informações do Controller
                var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor == null)
                    continue;

                // Checa se a classe do Controller é abstrata (como ApiCrudControllerBase<TEntity>)
                if (controllerActionDescriptor.ControllerTypeInfo.IsAbstract)
                {
                    // A rota relativa deve ser formatada como chave do swaggerDoc.Paths
                    var path = "/" + apiDescription.RelativePath.TrimEnd('/');

                    // Adiciona o caminho completo à lista de rotas a remover
                    // Rotas OData e padrões podem ter caminhos complexos
                    if (!pathsToRemove.Contains(path))
                    {
                        pathsToRemove.Add(path);
                    }
                }
            }

            // 2. Remover os caminhos identificados do documento Swagger
            foreach (var pathKey in pathsToRemove)
            {
                // Remove a chave do dicionário Paths
                swaggerDoc.Paths.Remove(pathKey);
            }
        }
    }
}