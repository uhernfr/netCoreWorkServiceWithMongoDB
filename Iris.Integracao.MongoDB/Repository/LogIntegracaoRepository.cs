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
    public class LogIntegracaoRepository : RepositoryMongo<Agenda>, ILogIntegracaoRepository
    {        
        protected readonly IMongoCollection<LogIntegracao> collection;
        private readonly IMongoDatabase _db;
        public LogIntegracaoRepository(IMongoClient client, IMongoDatabase db) : base(client, db)
        {            
            collection = db.GetCollection<LogIntegracao>("LogIntegracao");                       
            _db = db;
        }

        //public Agenda BuscarIntegracao(string codUsuario, DateTime dtInicioIntegracao, DateTime dtFimIntegracao)
        //{
        //    return collection.Find(a => a.DataPrevistaAtendimento >= dtInicioIntegracao
        //    && a.DataPrevistaAtendimento <= dtFimIntegracao
        //    && a.CodigoPessoa == codUsuario
        //    ).FirstOrDefault();
        //}

      
        public async Task GravarLogAsync(IList<LogIntegracao> lstLog)
        {
            try
            {
                await collection.InsertManyAsync(lstLog);
            }catch(Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
