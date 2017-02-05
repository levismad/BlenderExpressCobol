using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Threading;
using Levismad.Dominios.Entrada;
using Levismad.Dominios.Saida;
using Levismad.Contratos.Interfaces;
using Levismad.Framework;
using Levismad.Repositorio;
using log4net;

namespace Levismad.Objetos
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Servico2 : IServico2
    {

        public SaidaServico2 MetodoServico2(EntradaServico2 entrada)
        {
            var config = new ConfiguracaoServico();

            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ThreadContext.Properties["EventID"] = 2;
            var culture = new CultureInfo(config.DefaultCulture);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var saida = new SaidaServico2
            {
                Resultado = true,
                Mensagem = "OK - Confirmado"
            };

            var externalizarExcecao = config.ExternalizarExcecao;
            var repositorio = new RegrasRepositorio(entrada);

            try
            {
                
                saida = repositorio.Validar();

                if (!saida.Resultado)
                {
                    throw new ValidacaoException(saida.Mensagem, saida.CodigoRetorno);
                }
                
                return saida;
            }
            catch (ValidacaoException ex)
            {

                saida.Resultado = false;
                saida.Mensagem = externalizarExcecao ? Logger.FlattenException(ex) : "NOK - Não confirmado";
                saida.Retorno = null;
                saida.CodigoRetorno = ex.ErrorCode;
                log.Error(Logger.FlattenException(ex));

            }
            catch (Exception ex)
            {

                saida.Resultado = false;
                saida.Mensagem = externalizarExcecao ? Logger.FlattenException(ex) : "NOK - Não confirmado";
                saida.Retorno = null;
                saida.CodigoRetorno = 500;
                log.Error(Logger.FlattenException(ex));
            }
            return saida;

        }
    }
}
