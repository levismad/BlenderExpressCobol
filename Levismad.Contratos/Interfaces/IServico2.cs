using System.ServiceModel;
using Levismad.Dominios.Entrada;
using Levismad.Dominios.Saida;

namespace Levismad.Contratos.Interfaces
{
    [ServiceContract]
    public interface IServico2
    {

        [OperationContract]
        SaidaServico2 MetodoServico2(EntradaServico2 entrada);
    }
}
