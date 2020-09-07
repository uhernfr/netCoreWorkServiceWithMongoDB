using FluentValidation.Results;
using Integracao.MongoDB.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.MongoDB.Model
{
    public class Entity : IEntity
    {       
        [BsonId]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTimeOffset.Now.DateTime;
        public bool IsRemoved { get; set; } = false;
        public int Version { get; set; }
        public virtual ValidationResult ValidationResult { get; protected set; }
    }
}
