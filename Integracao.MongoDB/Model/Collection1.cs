using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.MongoDB.Model
{
    public class Collection1 : Entity
    {      
        public string Codigo { get; set; }
        public string Nome { get; set; }

        public bool Ativo { get; set; }

    }
}
