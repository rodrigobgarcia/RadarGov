using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.IReposoitorio
{
    public interface IBaseValidacao
    {
        public bool ValidateNome(string nome);

        public bool ValidateEmail(string email);

        public bool ValidatePassword(string password);

        public bool ValidateCnpj(string cnpj);


    }
}
