﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarGov.Dominio.DTOs
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public UsuarioDto(string nome, string email, string senha)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }
    }
}
