using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using RSK.API.Extensoes;
using RSK.Dominio.Autorizacao.Interfaces;
using RSK.Dominio.Autorizacao.Servicos;
using RSK.Dominio.Entidades;
using RSK.Dominio.Interfaces;
using RSK.Dominio.IRepositorios;
using RSK.Dominio.Notificacoes.Interfaces;
using RSK.Dominio.Notificacoes.Servicos;
using RSK.Dominio.Servicos;
using RSK.Infraestrutura.Dados;
using RSK.Infraestrutura.Repositorios;
using RSK.Infraestrutura.Servicos;
using System.Text;
using RSK.Dominio.Autorizacao.Entidades;
using Microsoft.OpenApi.Models;
using Quartz;
using RadarGov.Dominio.Entidades;
using RadarGov.Infraestrutura;
using RadarGov.Infraestrutura.Repositorios;
using RadarGov.Dominio.Servicos;
using RadarGov.API.Extensoes;
using RadarGov.Dominio.Interfaces;
using RadarGov.Integracoes.RadarHub;


namespace RadarGov.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            // ======= Servi�os de dom�nio =======
            builder.Services.AddScoped<EmpresaServico>();


            // ======= Agendamentos ========
            builder.Services.AddQuartz(); // Registra Quartz
            builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            //builder.Services.AdicionarJobServico<ModalidadeServico>(
            //    nomeJob: "ImportacaoModalidades",
            //    metodo: "ImportarAsync",
            //    tipoAgendamento: TipoAgendamento.Semanal,
            //    horaInicio: new TimeOnly(3, 0)
            //);



            builder.Services.AddScoped<IRepositorioBaseAssincrono<Empresa>, RepositorioBaseAssincrono<Empresa, RadarGovDbContext>>();

            // ======= Reposit�rios e servi�os base =======
            builder.Services.AddScoped(typeof(IRepositorioBaseAssincrono<>), typeof(RepositorioBaseRadarGov<>));
            builder.Services.AddScoped(typeof(IServicoConsultaBase<>), typeof(ServicoConsultaBase<>));
            builder.Services.AddScoped(typeof(IServicoCrudBase<>), typeof(ServicoCrudBase<>));
            builder.Services.AddScoped(typeof(IServicoImportacaoTerceiro<>), typeof(ServicoImportacaoTerceiro<>));

            // ======= Notifica��es =======
            builder.Services.AddScoped<IServicoUsuario, ServicoUsuario>();
            builder.Services.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();


            // ======= Transa��o =======
            builder.Services.AddScoped<ITransacao, TransacaoEF<RadarGovDbContext>>();

            // ======= Autoriza��o =======
            builder.Services.AddScoped<IHasher, Sha256Hasher>();
            builder.Services.AddScoped<IRepositorioAplicacaoPermitida, RepositorioAplicacaoPermitida<RadarGovDbContext>>();
            builder.Services.AddScoped<IServicoAplicacaoPermitidaPermissao, ServicoAplicacaoPermitidaPermissao>();
            builder.Services.AddScoped<IServicoVerificacaoAplicacao, ServicoVerificacaoAplicacao>();


            builder.Services.AddHttpClient<IRadarHubIntegracaoServico, RadarHubClient>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("User-Agent", "RadarHubClient/1.0");
            });

            builder.Services.AddScoped<
                IRepositorioBaseAssincrono<AplicacaoPermitida>,
                RepositorioBaseRadarGov<AplicacaoPermitida>
            >();




            // Registra a classe concreta
            builder.Services.AddScoped<ServicoMensagem>();

            // Ambas as interfaces usam a mesma inst�ncia
            builder.Services.AddScoped<IServicoMensagem>(sp => sp.GetRequiredService<ServicoMensagem>());
            builder.Services.AddScoped<INotificador>(sp => sp.GetRequiredService<ServicoMensagem>());


            // ======= Servi�os RSK =======
            builder.Services.AdicionarServicosRSK();

            // ======= Controllers =======
            builder.Services.AddControllers()
            //.AddApplicationPart(typeof(RSK.API.Controllers.UsuarioController).Assembly)
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            })
           .AddOData(opt =>
           {
               opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100);
               opt.AddRouteComponents("api", GetEdmModel());
           });


            // ======= Swagger =======
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerIgnoreAbstractControllerFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                // Define o esquema de seguran�a Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT no campo abaixo: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Aplica o Bearer a todos os endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });


            var app = builder.Build();
            //using (var scope = app.Services.CreateScope())
            //{
            //    var contexto = scope.ServiceProvider.GetRequiredService<RadarHubDbContext>();
            //    Console.WriteLine($"Conex�o: {contexto.Database.GetConnectionString()}");

            //    contexto.Add(new Modalidade { IdTerceiro = "teste", Nome = "TesteDireto" });
            //    var result = contexto.SaveChanges();
            //    Console.WriteLine($"{result} registro(s) salvo(s)!");
            //}
           



            // ======= Middleware pipeline =======
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");            // Habilita CORS
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseODataRouteDebug();

            app.MapControllers();
            app.Run();
        }


        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            // Configura��o manual de cada EntitySet (conjunto de entidades) para OData,
            // conforme solicitado. � crucial chamar .HasKey(e => e.Id) para
            // garantir que o OData reconhe�a a chave prim�ria herdada da EntidadeBase.

            // Entidades de RadarHub.Dominio.Entidades
            //builder.EntitySet<Empresa>("Empresa").EntityType.HasKey(e => e.Id);
            builder.EntitySet<UsuarioBase>("UsuarioBase").EntityType.HasKey(e => e.Id);

            // NOTE: A classe EntidadeBaseImportacaoTerceiro n�o � registrada pois � uma classe base
            // e n�o deve ser um EntitySet consult�vel.

            return builder.GetEdmModel();
        }

    }
}
