using System.Runtime.Serialization;

namespace Levismad.Dominios.Saida
{
    public interface ISaidaPadrao
    {
        bool Resultado { get; set; }
        string Mensagem { get; set; }

    }
    [DataContract]
    public class SaidaPadraoComCodigo : ISaidaPadrao
    {
        public SaidaPadraoComCodigo()
        {
            Resultado = true;
        }
        [DataMember]
        public bool Resultado { get; set; }
        [DataMember]
        public string Mensagem { get; set; }
        [DataMember]
        public int? CodigoRetorno { get; set; }

    }
}
