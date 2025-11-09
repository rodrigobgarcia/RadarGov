using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RadarGov.Dominio.Entidades;
using RadarGov.Dominio.Servicos;
using RadarGov.Infraestrutura;
using RadarGov.Infraestrutura.Repositorios;
using RadarGov.Integracoes.Gemini;
using RSK.API.Extensoes;
using RSK.Dominio.IRepositorios;
using RSK.Dominio.Notificacoes.Interfaces;
using RSK.Dominio.Notificacoes.Servicos;
using RSK.Infraestrutura.Dados;
using RSK.Infraestrutura.Repositorios;
using System.Text;

namespace RadarGov.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var apiKey = builder.Configuration["GoogleApi:ApiKey"];

            
            builder.Services.AddSingleton(new GeminiApiClient(apiKey));
            builder.Services.AddScoped<SegmentoClassifierService>();


            

            var configuration = builder.Configuration;

            // ======= DbContext =======
            builder.Services.AddDbContext<RadarGovDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("RadarGovDb"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("RadarGovDb"))
                )
            );


            // ======= CORS =======
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // ======= JWT Authentication =======
            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            // ======= Serviços de domínio =======
            builder.Services.AddScoped<EmpresaServico>();


            builder.Services.AddScoped<IRepositorioBaseAssincrono<Empresa>, RepositorioBaseAssincrono<Empresa, RadarGovDbContext>>();
            
            
            // ======= Repositórios e serviços base =======
            builder.Services.AddScoped(typeof(IRepositorioBaseAssincrono<>), typeof(RepositorioBaseRadarGov<>));


            // Registra a classe concreta
            builder.Services.AddScoped<ServicoMensagem>();

            // Ambas as interfaces usam a mesma instância
            builder.Services.AddScoped<IServicoMensagem>(sp => sp.GetRequiredService<ServicoMensagem>());
            builder.Services.AddScoped<INotificador>(sp => sp.GetRequiredService<ServicoMensagem>());


            // ======= Transação =======
            builder.Services.AddScoped<ITransacao, TransacaoEF<RadarGovDbContext>>();


            // ======= Serviços RSK =======
            builder.Services.AdicionarServicosRSK();

            // Add services to the container.
            builder.Services.AddControllers();

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
