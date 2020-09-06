using Iris.Integracao.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Integracao.MongoDB.Interface
{    
      public interface ILogIntegracaoRepository
    {
        // public Task<Parametro> GetParametroAsync(string tipo);
        public Task GravarLogAsync(IList<LogIntegracao> lstLog);        
      
    }
}
