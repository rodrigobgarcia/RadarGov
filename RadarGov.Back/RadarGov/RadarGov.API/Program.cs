using RadarGov.Infraestrutura;
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
