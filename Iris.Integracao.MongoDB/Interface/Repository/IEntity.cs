using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Interface
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
