using AutoMapper;
using Iris.Integracao.MongoDB.Config;
using Iris.Integracao.MongoDB.Interface;
using Iris.Integracao.MongoDB.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using System;

namespace Iris.Integracao.Worker.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            try
            {
                Log.Information($"Starting {nameof(Iris.Integracao.Worker)}");

                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (string.IsNullOrEmpty(environment))
                    environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

                if (string.IsNullOrEmpty(environment))
                    environment = "local";

                string ENV_NAME = environment == "Development" ? "dev" : environment;
                string APP_SETTINGS_ENV = $"appsettings.{ENV_NAME}.json";

                
                Console.WriteLine($"Processo de Integracao Iris.Portaria - Ambiente: {ENV_NAME.ToUpper()}");
                Console.WriteLine($"APP_SETTINGS_ENV: {APP_SETTINGS_ENV.ToUpper()}");
                Console.WriteLine($"");

                CreateHostBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile($"appsettings.{ENV_NAME}.json", optional: true);
                })
                .Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" CW - Host terminated unexpectedly. -> {ex.Message}");
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment;
                    
                    services.AddAutoMapper(typeof(Program));


                    #region DI MongoDB
                    services.Configure<MongoDbContextConfig>(hostContext.Configuration.GetSection("MongoDb"));
                    services.AddSingleton<IMongoClient>(provider => new MongoClient(provider.GetRequiredService<IOptions<MongoDbContextConfig>>().Value.MongoConnectionString));
                    services.AddSingleton<IMongoDatabase>(provider =>
                        provider.GetRequiredService<IMongoClient>().GetDatabase(provider.GetRequiredService<IOptions<MongoDbContextConfig>>().Value.Database)
                    );
                    //repository
                 
                    services.AddSingleton<ILogIntegracaoRepository, LogIntegracaoRepository>();
                    services.AddSingleton<IConfigIntegracaoRepository, ConfigIntegracaoRepository>();
                                        
                    #endregion

                  
                    #region DI Workers - Integracões Ativas
                     
                    //Usuario-agenda 
                    services.AddHostedService<wsWorkerExemplo>();                  
                    
                                        
                    #endregion
                });
    }
}
