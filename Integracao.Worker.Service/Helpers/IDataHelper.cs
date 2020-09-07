using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.Worker.Service
{
    public interface IDataHelper
    {
        public DateTime GetDateUtcNow(DateTime date);

    }
}
