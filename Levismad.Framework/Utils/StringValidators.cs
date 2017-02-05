
namespace Levismad.Framework
{
    public static class StringValidators
    {
        public static bool ValidarTamanho(this string envio,int maximo) {
            if (string.IsNullOrEmpty(envio)) return true;
            return envio.Length <= maximo;
        }
        public static  bool ValidarEnvio(this string envio)
        {
            return !string.IsNullOrEmpty(envio);
        }

        public static string ToSafeString(this char? val)
        {
            return val?.ToString();
        }
    }
}
