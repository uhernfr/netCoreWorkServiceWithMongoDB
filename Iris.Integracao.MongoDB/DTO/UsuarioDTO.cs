using Iris.Integracao.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.DTO
{
    public class UsuarioDTO    
    {
        public List<Usuario> Usuarios = new List<Usuario>();        
        public string Mensagem { get; set; }
    }
}
