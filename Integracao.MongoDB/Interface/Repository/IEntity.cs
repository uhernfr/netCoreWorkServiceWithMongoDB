using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.MongoDB.Interface
{
    public interface IEntity
    {
        [BsonId]
        Guid Id { get; set; }
    }
}
