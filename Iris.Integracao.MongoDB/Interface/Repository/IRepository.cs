using Iris.Integracao.MongoDB.Model;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Iris.Integracao.MongoDB.Interface
{
    public interface IRepository<TEntity> where TEntity :Entity
    {        
        IMongoQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
