using Iris.Integracao.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Integracao.MongoDB.Interface
{    
      public interface IConfigIntegracaoRepository
    {       
        public ConfigIntegracao GetConfiguracoes(string tipoIntegracao);        
      
    }
}
