using Integracao.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.MongoDB.Interface
{    
      public interface ICollection2Repository
    {       
        public Collection2 GetDadosCollection2();
        public void Gravar(List<Collection2> entity);
    }
}
