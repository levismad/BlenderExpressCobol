using System;
using System.Collections.Generic;
using Levismad.Dominios.Entrada;
using Levismad.Dominios.Saida;
using Levismad.Framework;
using Levismad.Framework.Contratos;
using Levismad.Repositorio.Regras;

namespace Levismad.Repositorio
{
    public class RegrasRepositorio : AbstractRepository
    {
        EntradaServico2 _entrada;
        public RegrasRepositorio(EntradaServico2 entrada)
        {
            _entrada = entrada;
        }
        public SaidaServico2 Validar()
        {
            var saida = new SaidaServico2();
            var regrasValidacao = new List<IRegrasValidacao>() {
                new Regra99Validator(this)
            };

            try
            {
                regrasValidacao.ForEach(r =>
                {
                    r.Validar(ref _entrada);
                });


            }
            catch (ValidacaoException ex)
            {
                saida.Mensagem = ex.Message;
                saida.Resultado = !ex.TerminationError;
                saida.CodigoRetorno = ex.ErrorCode;
            }
            catch (Exception ex)
            {
                saida.Mensagem = ex.Message;
                saida.Resultado = false;
                saida.CodigoRetorno = 500;
            }
            return saida;
        }
    }
}
