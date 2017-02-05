using System;
using System.Globalization;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Threading;
using Levismad.Dominios.Entrada;
using Levismad.Dominios.Mainframe;
using Levismad.Dominios.Saida;
using Levismad.Contratos.Interfaces;
using Levismad.Framework;
using Levismad.Repositorio;
using log4net;

namespace Levismad.Objetos
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Servico1 : IServico1
    {

        public SaidaServico1 MetodoServico1(EntradaServico1 entrada)
        {
            var config = new ConfiguracaoServico();

            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ThreadContext.Properties["EventID"] = 2;

            var culture = new CultureInfo(config.DefaultCulture);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var saida = new SaidaServico1
            {
                Resultado = true,
                Mensagem = null
            };
            
            var externalizarExcecao = config.ExternalizarExcecao;
            try
            {
                var entrada01 = new EntradaCobol(config.CobolBanco, config.CobolUsuario, config.CobolSenha);

                // Serializar modelo cobol em uma string contínua.
                /*
                 * var envioCobol = entrada01.ToString();
                */

                // Realizar a chamada cobol e deserializar o retorno em um modelo <T> especifico
                var cobol = new CobolRepositorio();
                // ReSharper disable once UnusedVariable
                var saida01 = cobol.Execute(entrada01.ToString()).GetCobolModel<SaidaCobol>();

                saida.Retorno = new SaidaServico1Model();
                

            }
            catch (Exception ex)
            {
                saida.Resultado = false;
                saida.Mensagem = externalizarExcecao ? Logger.FlattenException(ex) : "Não foi possivel obter os dados.";
                saida.Retorno = null;
                log.Error(externalizarExcecao ? Logger.FlattenException(ex) : "Não foi possivel obter os dados.");

            }
            return saida;
        }
        

    }
}
