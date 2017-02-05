using System.Configuration;

namespace Levismad.Objetos
{
    public class ConfiguracaoServico
    {
        public string CobolHostName { get; set; }
        public string CobolAmbiente { get; set; }
        public string CobolBanco { get; set; }
        public string CobolUsuario { get; set; }
        public string CobolSenha { get; set; }
        public string DefaultCulture { get; set; }
        public bool ExternalizarExcecao => (!string.IsNullOrEmpty(_externalizarExcecao) && _externalizarExcecao == "true");

        // ReSharper disable once InconsistentNaming
        private string _externalizarExcecao { get; }

        public ConfiguracaoServico()
        {
            CobolHostName = ConfigurationManager.AppSettings["Levismad.COBOL.hostName"];
            CobolAmbiente = ConfigurationManager.AppSettings["Levismad.COBOL.ambiente"];

            CobolBanco = ConfigurationManager.AppSettings["Levismad.COBOL.banco"];
            CobolUsuario = ConfigurationManager.AppSettings["Levismad.COBOL.usuario"];
            CobolSenha = ConfigurationManager.AppSettings["Levismad.COBOL.senha"];

            DefaultCulture = ConfigurationManager.AppSettings["DefaultCulture"];
            _externalizarExcecao = ConfigurationManager.AppSettings["ExternalizarExcecao"];
        }
    }
}
