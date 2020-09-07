using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Integracao.MongoDB.Interface;
using Integracao.MongoDB.Model;
using MongoDB.Bson;

namespace Arquivos.Infra.Data.Repository
{
    public class RepositoryMongo<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;

        protected readonly IMongoCollection<TEntity> Collection;

        public RepositoryMongo(IMongoClient client, IMongoDatabase db, string collection = "")
        {
            collection = typeof(TEntity).Name;
            _client = client;
            _db = db;
            Collection = db.GetCollection<TEntity>(collection);
        }

        public IMongoQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) =>
            Collection.AsQueryable().Where(predicate);

        public IEnumerable<TEntity> GetAll() =>
            Collection.Find(_ => true).ToList();

        public void Insert(TEntity entity)
        {
            Collection.InsertOneAsync(entity);
        }
        public void Update(TEntity entity)
        {
            Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public void Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            Collection.DeleteOneAsync(filter);
        }

        public virtual TEntity GetById(Guid id)
        {
            var bytes = GuidConverter.ToBytes(id, GuidRepresentation.CSharpLegacy);
            var csuuid = new Guid(bytes);

            return Collection.Find(x => x.Id == csuuid).FirstOrDefault();
            //return GetAllRows().FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}
