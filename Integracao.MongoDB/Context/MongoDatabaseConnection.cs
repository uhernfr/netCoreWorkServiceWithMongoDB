﻿using Integracao.MongoDB.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integracao.MongoDB.Context
{
    public class MongoDatabaseConnection
    {
        private readonly IConfiguration _configuration;

        public MongoDatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MongoDbContextConfig GetConnection()
        {
            var config = new MongoDbContextConfig();
            _configuration.Bind("MongoDB", config);
            Console.WriteLine($"MongoConnString {config.MongoConnectionString}");
            return config;
        }

    }
}
