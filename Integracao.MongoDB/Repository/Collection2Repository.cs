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
    public class Collection2Repository : RepositoryMongo<Collection2>, ICollection2Repository
    {        
        protected readonly IMongoCollection<Collection2> collection;
        private readonly IMongoDatabase _db;
        public Collection2Repository(IMongoClient client, IMongoDatabase db) : base(client, db)
        {            
            collection = db.GetCollection<Collection2>("Collection2");                       
            _db = db;
        }

        public Collection2 GetDadosCollection2()
        {
            return collection.Find(q => q.Ativo == true
                                  ).FirstOrDefault();
        }

        public void Gravar(List<Collection2> entity)
        {
            foreach (var item in entity)
            {
                collection.InsertOne(item);
            }
        }
    }
}
