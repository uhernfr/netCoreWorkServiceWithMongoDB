using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TimeZoneConverter;

namespace Iris.Integracao.Worker.Service
{
    public class DataHelper : IDataHelper
    {
        public DataHelper()
        {

        }
        public DateTime GetDateUtcNow(DateTime date)
        {
            //SE for teste de desenvolvimento, ira pegar a hora da maquina local;
            var TesteDesenvolvimento = false;
            if (TesteDesenvolvimento)
            {
                return date;
            }

            string tz = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                tz = TZConvert.IanaToWindows("America/Sao_Paulo");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                tz = TZConvert.WindowsToIana("E. South America Standard Time");
            }

            var cetZone = TimeZoneInfo.FindSystemTimeZoneById(tz);

            return TimeZoneInfo.ConvertTime(date, cetZone);
        }
    }

    enum EnumTempoExecucao : int
    {
        Diaria = 86400000,
        Semanal = 604800000,       
        Hora = 3600000,
        Minuto = 60000
    }
}
