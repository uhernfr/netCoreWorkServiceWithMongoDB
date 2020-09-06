using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Model
{
    public class ConfigIntegracao : Entity
    {
        public string Nome { get; set; }
        public string TipoIntegracao { get; set; }        
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
     

    }
}
