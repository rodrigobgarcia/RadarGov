using Quartz;
using RadarGov.API.Jobs;
using RadarGov.Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace RadarGov.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("JobGetLicitacoes");

                q.AddJob<JobGetLicitacoes>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts.ForJob(jobKey)
                    .WithIdentity("JobGetLicitacoes-trigger")
                    .WithCronSchedule("0/1 * * * * ?"));
            });

            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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
