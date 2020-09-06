using Arquivos.Infra.Data.Repository;
using Iris.Integracao.MongoDB.Interface;
using Iris.Integracao.MongoDB.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iris.Integracao.MongoDB.Repository
{
    public class Collection1Repository : RepositoryMongo<Collection1>, ICollection1Repository
    {        
        protected readonly IMongoCollection<Collection1> collection;
        private readonly IMongoDatabase _db;
        public Collection1Repository(IMongoClient client, IMongoDatabase db) : base(client, db)
        {            
            collection = db.GetCollection<Collection1>("Collection1");                       
            _db = db;
        }

        public Collection1 GetDadosCollection1()
        {
            return collection.Find(q => q.Ativo == true
                                  ).FirstOrDefault();
        }
    }
}
