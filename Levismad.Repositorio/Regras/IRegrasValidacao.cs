using Levismad.Dominios.Entrada;

namespace Levismad.Repositorio.Regras
{
    public interface IRegrasValidacao
    {
        void Validar(ref EntradaServico2 entrada);
    }
}
