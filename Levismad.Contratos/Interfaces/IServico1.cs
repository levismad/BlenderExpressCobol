using System.ServiceModel;
using Levismad.Dominios.Entrada;
using Levismad.Dominios.Saida;

namespace Levismad.Contratos.Interfaces
{
    [ServiceContract]
    public interface IServico1
    {
        [OperationContract]
        SaidaServico1 MetodoServico1(EntradaServico1 entrada);
    }
}
