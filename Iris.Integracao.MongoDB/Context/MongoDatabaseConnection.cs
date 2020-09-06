using Iris.Integracao.MongoDB.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.Integracao.MongoDB.Context
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

        public MongoDbContextConfig GetGuaritaConnection()
        {
            var config = new MongoDbContextConfig();
            _configuration.Bind("MongoDB", config);
            Console.WriteLine($"MongoConnStringGuarita {config.MongoConnectionString}");
            return config;
        }
    }
}
