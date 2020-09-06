using FluentValidation.Results;
using Iris.Integracao.MongoDB.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Model
{
    public class Entity : IEntity
    {       
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTimeOffset.Now.DateTime;
        public bool IsRemoved { get; set; } = false;
        public int Version { get; set; }
        public virtual ValidationResult ValidationResult { get; protected set; }
    }
}
