using AutoMapper;
using Iris.Integracao.MongoDB.DTO;
using Iris.Integracao.MongoDB.Interface;
using Iris.Integracao.MongoDB.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iris.Integracao.Worker.Service
{
    public class wsWorkerExemplo : BackgroundService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<wsWorkerExemplo> _logger;
  
        private readonly ICollection1Repository _collection1Repository;
        private readonly ILogIntegracaoRepository _logIntegracao;
        private readonly IConfigIntegracaoRepository _configIntegracaoRepository;
        private readonly ICollection1Repository collection1Repository;

        public wsWorkerExemplo(IMapper mapper,
            ILogger<wsWorkerExemplo> logger,
            ICollection1Repository collection1Repository,
            ILogIntegracaoRepository logIntegracao,
            IConfigIntegracaoRepository configIntegracaoRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _collection1Repository = collection1Repository;
            _logIntegracao = logIntegracao;
            _configIntegracaoRepository = configIntegracaoRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var strPeriodoDescricao = "Semanal - Sex 21h";
            // _logger.LogInformation($"Worker wsAgendaUsuarioLegado - running at: {DateTimeOffset.Now.AddHours(-3)}");
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            while (!stoppingToken.IsCancellationRequested)
            {
                //Configuracoes de Execução
                var processoAtivo = false;
              
                try
                {
                    var configIntegracao = _configIntegracaoRepository.GetConfiguracoes("wsWorkerExemplo");
                    if (configIntegracao == null)
                    {
                        Console.WriteLine($" -> Atenção: PROBLEMAS AO ACESSAR MongoDB. Registro de Configuração não encontrado.");
                    }
                    else
                    {
                        processoAtivo = configIntegracao.Ativo;                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" -> Atenção: PROBLEMAS AO ACESSAR MongoDB. Detalhes: {ex.Message.Substring(1, 100)}");
                    cancellationTokenSource.Cancel();
                }
                //---

                var dataBase = DateTimeOffset.Now.AddHours(-3);
                var msgLog = new StringBuilder();

                //if (dataBase.Hour == 21 && dataBase.Date.DayOfWeek == DayOfWeek.Friday || debug)
                if (processoAtivo)
                {
                    msgLog.AppendLine($"{DateTimeOffset.Now.AddHours(-3)} - Processo Copia Collection 1 para Colection 2 (Minuto) iniciado.");
                    
                    var ret = BuscaDadosCollection1(dataBase.DateTime, dataBase.DateTime.AddDays(1));
                    msgLog.Append(ret.Mensagem);
                    var retLstCollection1 = ret.Usuarios;

                    try
                    {
                        msgLog.AppendLine($"-> Gravando dados MongoDB");

                        var lstUsuarios = retLstCollection1.Select(x => new Usuario()
                        {
                            Codigo = x.Codigo,
                            Nome = x.Nome
                        }
                        ).ToList();                
                   

                        if (lstUsuarios.Count() > 0)
                        {
                            //msgLog.Append(_UsuarioMongoDBService.GravarListaAgendaUsuario(lstUsuarios, "wsAgendaUsuarioLegado"));
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

                    var logIntegracao = new List<LogIntegracao>
                                    {
                                        new LogIntegracao()
                                        {
                                            TipoIntegracao = "wsAgendaUsuarioLegado",
                                            TipoRegistro = "Processado",
                                            Periodo = strPeriodoDescricao,
                                            Mensagem = msgLog.ToString()
                                        }
                                    };
                    await _logIntegracao.GravarLogAsync(logIntegracao);
                }

                else
                {
                    msgLog.AppendLine($"-> Processo em execucao. Busca de dados:  {strPeriodoDescricao}.");
                    _logger.LogInformation(msgLog.ToString());
                }
                await Task.Delay((int)EnumTempoExecucao.Minuto, stoppingToken);
            }
        }

        private UsuarioDTO BuscaDadosCollection1(DateTime dataInicio, DateTime dataFim)
        {
            var msgLog = new StringBuilder();
            msgLog.AppendLine("-> Buscando dados Collection 1");
            msgLog.AppendLine($"Parametros - Inicio: {dataInicio} - Data Fim: { dataFim}");
            var usuarios = new List<Usuario>() {
            new Usuario(){ Codigo = "7780000", Nome = "Francisco" }
            };
            

            return new UsuarioDTO()
            {
                Mensagem = "OK",
                Usuarios = usuarios
            };

                //try
                //{
                //        var configIntegracao = _configIntegracaoRepository.GetConfiguracoes("wsWorkerExemplo");
                //        if (configIntegracao == null)
                //        {
                //            Console.WriteLine($" -> Atenção: PROBLEMAS AO ACESSAR MongoDB. Registro de Configuração não encontrado.");
                //        }
                //        else
                //        {
                //            processoAtivo = configIntegracao.Ativo;                       
                //        }
                //}
                //catch (Exception ex)
                //{
                //    msgLog.AppendLine($"Ocorreu um erro no processamento: {ex.Message}");
                //    _logger.LogInformation(msgLog.ToString());
                //    return new UsuarioAgendaDTO()
                //    {
                //        Mensagem = msgLog
                //    };
                //}
            }
    }
}
