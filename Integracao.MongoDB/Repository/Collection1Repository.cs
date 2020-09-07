using Arquivos.Infra.Data.Repository;
using Integracao.MongoDB.Interface;
using Integracao.MongoDB.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracao.MongoDB.Repository
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

        public List<Collection1> GetDadosCollection1()
        {
            return collection.Find(q => q.Ativo == true).ToList();
        }

        public void Gravar(Collection1 entity)
        {           
            collection.InsertOne(entity);           
        }

       
    }
}
