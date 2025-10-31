using Microsoft.EntityFrameworkCore;
using Quartz;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Interfaces;
using RadarGov.Dominio.IReposoitorio;
using RadarGov.Dominio.Notificacoes.Servicos;
using RadarGov.Dominio.Servicos;
using RadarGov.Infraestrutura;
using RadarGov.Infraestrutura.Interfaces;
using RadarGov.Infraestrutura.Repositorios;
using RadarGov.Integracoes.Pnc;
using RadarGov.Integracoes.Gemini;

namespace RadarGov.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.RegisterFromNamespaces(
            //    assemblies: new[]
            //    {
            //        "RadarGov.Dominio",
            //        "RadarGov.Infraestrutura"
            //    },
            //    namespaces: new[]
            //    {
            //        "RadarGov.Dominio.Servicos",
            //        "RadarGov.Dominio.Notificacoes.Servicos",
            //        "RadarGov.Infraestrutura.Repositorios"
            //    },
            //    enableLogging: true
            //);
            var apiKey = builder.Configuration["GoogleApi:ApiKey"];

            builder.Services.AddScoped<ModalidadeServico>();
            builder.Services.AddScoped<MunicipioServico>();
            builder.Services.AddScoped<OrgaoServico>();
            builder.Services.AddScoped<UnidadeServico>();
            builder.Services.AddScoped<UfsServico>();
            builder.Services.AddScoped<PoderServico>();
            builder.Services.AddScoped<TipoServico>();
            builder.Services.AddScoped<EsferaServico>();
            builder.Services.AddScoped<TipoMargemPreferenciaServico>();
            builder.Services.AddScoped<FonteOrcamentariaServico>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Modalidade>, ImportacaoTerceiroRepositorio<Modalidade>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Orgao>, ImportacaoTerceiroRepositorio<Orgao>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Unidade>, ImportacaoTerceiroRepositorio<Unidade>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Ufs>, ImportacaoTerceiroRepositorio<Ufs>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Municipio>, ImportacaoTerceiroRepositorio<Municipio>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Poder>, ImportacaoTerceiroRepositorio<Poder>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Tipo>, ImportacaoTerceiroRepositorio<Tipo>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<Esfera>, ImportacaoTerceiroRepositorio<Esfera>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<TipoMargemPreferencia>, ImportacaoTerceiroRepositorio<TipoMargemPreferencia>>();
            builder.Services.AddScoped<IImportacaoTerceiroRepositorio<FonteOrcamentaria>, ImportacaoTerceiroRepositorio<FonteOrcamentaria>>();
            builder.Services.AddScoped<MensagemServico>();
            builder.Services.AddHttpClient<Pncp>();
            builder.Services.AddScoped<UsuarioServico>();
            builder.Services.AddScoped<EmpresaServico>();
            builder.Services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
            builder.Services.AddScoped<IBaseValidacao, BaseValidacao>();

            builder.Services.AddSingleton(new GeminiApiClient(apiKey));
            builder.Services.AddScoped<SegmentoClassifierService>();


            // Add services to the container.
            builder.Services.AddControllers();

            // Adiciona DbContext com connection string do appsettings.json
            builder.Services.AddDbContext<RadarGovDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("RadarGovDb"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("RadarGovDb"))
                )
            );

            // Configura Quartz
            //builder.Services.AddQuartz(q =>
            //{
            //    var jobKey = new JobKey("JobGetLicitacoes");

            //    q.AddJob<JobGetLicitacoes>(opts => opts.WithIdentity(jobKey));

            //    q.AddTrigger(opts => opts.ForJob(jobKey)
            //        .WithIdentity("JobGetLicitacoes-trigger")
            //        .WithCronSchedule("0/1 * * * * ?"));
            //});

            //builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
