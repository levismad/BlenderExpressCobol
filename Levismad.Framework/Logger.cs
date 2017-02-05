using System.Diagnostics;
using System.Text;

namespace System
{

    public static class Logger
    { 
        /*
            eventcreate /ID 2 /L APPLICATION /T ERROR /SO "Levismad WCF" /D "Dummy log message"
        */
        private const string Eventlogname = "Levismad WCF - Gravação Proposta";
        private const string Sourcelogname = "Webservice_log";

        public static void Error(string message, string module)
        {
            WriteEntry(message, "error");
        }

        public static void Error(Exception ex)
        {
            WriteEntry(ex, "error");
        }

        public static void Warning(string message, string module)
        {
            WriteEntry(message, "warning");
        }

        public static void Info(string message, string module)
        {
            WriteEntry(message, "info");
        }

        private static void WriteEntry(Exception message, string type = "INFO")
        {
            Trace.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {type} : {FlattenException(message)}");
        }
        private static void WriteEntry(string message, string type = "INFO")
        {
            Trace.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {type} : {message}");
        }
        public static string FlattenException(Exception exception)
        {
            //CreateSourceIfNotExists();

            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.Append(exception.Message);
                //stringBuilder.AppendLine(exception.StackTrace);
                
                exception = exception.InnerException;
                if (exception != null) stringBuilder.Append(" Detalhes : ");
            }

            return stringBuilder.ToString();
        }
        public static string IncludeCobolInput(string envioCobol, Exception exception)
        {
            //CreateSourceIfNotExists();
            if (string.IsNullOrEmpty(envioCobol)) envioCobol = "String não foi gerada";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($" String de envio ISISF004 : \"{envioCobol}\" \n");
            while (exception != null)
            {
                stringBuilder.Append(exception.Message);
                //stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
                if (exception != null) stringBuilder.Append(" Detalhes : ");
            }

            return stringBuilder.ToString();
        }

        public static void DeleteSource()
        {
            if (!EventLog.SourceExists(Sourcelogname)) return;
            var evLog = new EventLog { Source = Sourcelogname };
            if (evLog.Log != Eventlogname)
            {
                EventLog.DeleteEventSource(Sourcelogname);
            }
        }
        private static bool CreateSourceIfNotExists()
        {
            if (EventLog.SourceExists(Sourcelogname)) return EventLog.SourceExists(Sourcelogname);
            EventLog.CreateEventSource(Sourcelogname, Eventlogname);
            EventLog.WriteEntry(Sourcelogname, $"Event Log Created '{Eventlogname}'/'{Sourcelogname}'", EventLogEntryType.Information);

            return EventLog.SourceExists(Sourcelogname);
        }

        public static void WriteEventToMyLog(string text, EventLogEntryType type)
        {
            if (CreateSourceIfNotExists())
            {
                EventLog.WriteEntry(Sourcelogname, text, type);
            }
        }
    }
}