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
    public class ConfigIntegracaoRepository : RepositoryMongo<ConfigIntegracao>, IConfigIntegracaoRepository
    {        
        protected readonly IMongoCollection<ConfigIntegracao> collection;
        private readonly IMongoDatabase _db;
        public ConfigIntegracaoRepository(IMongoClient client, IMongoDatabase db) : base(client, db)
        {            
            collection = db.GetCollection<ConfigIntegracao>("ConfigIntegracao");                       
            _db = db;
        }

        public ConfigIntegracao GetConfiguracoes(string tipoIntegracao)
        {
            return collection.Find(q => q.TipoIntegracao == tipoIntegracao
                                  ).FirstOrDefault();
        }
    }
}
