using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.Worker.Service
{
    public interface IDataHelper
    {
        public DateTime GetDateUtcNow(DateTime date);

    }
}
