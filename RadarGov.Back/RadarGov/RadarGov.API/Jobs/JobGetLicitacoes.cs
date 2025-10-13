using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace RadarGov.API.Jobs
{
    public class JobGetLicitacoes : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("// Requisição de Licitaçãoes (RAYSSA)");
            return Task.CompletedTask;
        }
    }
}
