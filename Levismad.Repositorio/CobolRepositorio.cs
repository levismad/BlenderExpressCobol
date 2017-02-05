using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;

namespace Levismad.Repositorio
{
    public class CobolRepositorio
    {
        private string HostName { get; }
        private string Ambiente { get; }
        public CobolRepositorio()
        {
            HostName = ConfigurationManager.AppSettings["Levismad.COBOL.hostName"];
            Ambiente = HttpUtility.UrlDecode(ConfigurationManager.AppSettings["Levismad.COBOL.ambiente"]);

        }
        public string Execute(string dados)
        {
            var dadosUri = Uri.EscapeDataString(dados);
            var webClient = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            try
            {
                return webClient.DownloadString($"{HostName}?{Ambiente}{dadosUri}");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel acessar o servidor MICROFOCUS",ex);
            }

        }
    }
}
