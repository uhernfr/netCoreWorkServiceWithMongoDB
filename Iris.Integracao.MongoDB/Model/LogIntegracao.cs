using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Model
{
    public class LogIntegracao : Entity
    {        
        public string TipoIntegracao { get; set; }
        public string Periodo { get; set; }
        public string TipoRegistro { get; set; }
        public string CampoChave { get; set; }
        public string ValorCampoChave { get; set; }
        public string Mensagem { get; set; }

    }
}
