﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Config
{
    public class MongoDbContextConfig
    {
        public string MongoConnectionString { get; set; }
        public string Database { get; set; }
    }
}
