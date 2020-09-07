using AutoMapper;
using Integracao.MongoDB.Interface;
using Integracao.MongoDB.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Integracao.Worker.Service
{
    public class wsWorkerExemplo : BackgroundService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<wsWorkerExemplo> _logger;
  
        private readonly ICollection1Repository _collection1Repository;
        private readonly ICollection2Repository _collection2Repository;       
        

        public wsWorkerExemplo(IMapper mapper,
            ILogger<wsWorkerExemplo> logger,
            ICollection1Repository collection1Repository,
            ICollection2Repository collection2Repository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _collection1Repository = collection1Repository;
            _collection2Repository = collection2Repository; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            #region criacao de registros exemplo em collection1
        
            for (var i = 0;i<20;i++)
            {
                _collection1Repository.Gravar(new Collection1()
                {
                    Codigo = i.ToString(),
                    Nome = $"Registro{i} Collection 1", Ativo = true
                });
                ;
            }

            #endregion

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            while (!stoppingToken.IsCancellationRequested)
            {               
                var dataBase = DateTimeOffset.Now.AddHours(-3);
                var msgLog = new StringBuilder();                
               
                msgLog.AppendLine($"{DateTimeOffset.Now.AddHours(-3)} - Processo Copia Collection 1 para Colection 2 (Minuto) iniciado.");
                    
                var retLstCollection1 = BuscaDadosCollection1(dataBase.DateTime, dataBase.DateTime.AddDays(1));
             
                try
                {
                    msgLog.AppendLine($"-> Gravando dados MongoDB");                   

                    if (retLstCollection1.Count() > 0)
                    {
                        
                         var dto = retLstCollection1.Select(x => new Collection2()
                             {
                                 Codigo = x.Codigo,
                                 Nome = x.Nome
                             }
                        ).ToList();

                        msgLog.AppendLine($"-> {dto.Count()} registros copiados.");
                        _collection2Repository.Gravar(dto);
                    }
                    else
                    {
                        msgLog.AppendLine("-> Nao ha registros para Importar");
                    }

                    msgLog.AppendLine($"{DateTimeOffset.Now.AddHours(-3)} - Processo Concluido");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problema com -> {ex}", ex);
                }

                _logger.LogInformation(msgLog.ToString());
               
                await Task.Delay((int)EnumTempoExecucao.Minuto, stoppingToken);
            }
        }

        private List<Collection1> BuscaDadosCollection1(DateTime dataInicio, DateTime dataFim)
        {
            var msgLog = new StringBuilder();
            msgLog.AppendLine("-> Buscando dados Collection 1");
            msgLog.AppendLine($"Parametros - Inicio: {dataInicio} - Data Fim: { dataFim}");

            var lstCollection1 = _collection1Repository.GetDadosCollection1();
           
            return lstCollection1;
               
        }
    }
}
