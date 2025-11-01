using RadarGov.Dominio.IReposoitorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Infraestrutura.Repositorios
{
    public class BaseValidacao : IBaseValidacao
    {
        public bool ValidateNome(string nome)
        {
            return false;
        }

        public bool ValidateEmail(string email)
        {
            return false;

        }

        public bool ValidatePassword(string password)
        {
            return false;
        }

        public bool ValidateCnpj(string cnpj)
        {
            return false;

        }
    }
}
