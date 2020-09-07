using Integracao.MongoDB.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Integracao.MongoDB.Interface
{    
      public interface ICollection1Repository
    {
        public List<Collection1> GetDadosCollection1();
        public void Gravar(Collection1 entity);


    }
}
