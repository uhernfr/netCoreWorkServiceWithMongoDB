using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Model
{
    public class Usuario: Entity    
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}
