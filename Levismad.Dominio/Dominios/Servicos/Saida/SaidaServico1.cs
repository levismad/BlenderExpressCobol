using System.Runtime.Serialization;

namespace Levismad.Dominios.Saida
{
    [DataContract]
    public class SaidaServico1 : SaidaPadraoComCodigo
    {
        [DataMember]
        public SaidaServico1Model Retorno { get; set; }
    }
    [DataContract]
    public class SaidaServico1Model
    {
    }
    
}
