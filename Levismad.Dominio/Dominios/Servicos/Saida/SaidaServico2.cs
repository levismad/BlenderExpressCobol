using System.Runtime.Serialization;

namespace Levismad.Dominios.Saida
{
    [DataContract]
    public class SaidaServico2 : SaidaPadraoComCodigo
    {
        [DataMember]
        public SaidaServico2Model Retorno { get; set; }
    }
    [DataContract]
    public class SaidaServico2Model
    {

    }
}
