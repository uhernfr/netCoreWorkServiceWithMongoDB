using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.MongoDB.Model
{
    public class Agenda:Entity
    {
        public string Nome { get; set; }
        public string TipoIntegraca { get; set; }
        public string Descricao { get; set; }
        public string Ativo { get; set; }
    }
}
