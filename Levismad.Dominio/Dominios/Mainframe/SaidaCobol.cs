using System.Collections.Generic;
using Levismad.Framework;
using Levismad.Framework.Annotations;

namespace Levismad.Dominios.Mainframe
{
    public class SaidaCobol : CobolModel
    {
        public SaidaCobol()
        {
            Itens = new List<RetornoChamadaCobol>();
        }

        [CobolColumn(0, 6, TipoConversao.Texto, TipoPadding.Esquerda, '0')]
        public string CodigoInical { get; set; } 

        [CobolColumn(1, 3, TipoConversao.Texto, TipoPadding.Esquerda)]
        public string CodigoDaFuncao { get; set; } 

        [CobolColumn(2, 5, TipoConversao.Texto, TipoPadding.Esquerda, '0',TipoConversaoSaida.Texto,0,true)]
        public string CodigoDoErro { get; set; } 

        [CobolColumn(3,typeof(RetornoChamadaCobol), "LISTA_VALORES")]
        public List<RetornoChamadaCobol> Itens { get; set; } 

        [CobolColumn(4, 10, TipoConversao.Texto, TipoPadding.Esquerda)]
        public string FimDaString { get; set; } 



    } 


    public class RetornoChamadaCobol
    {
        [CobolColumn(1, 3, TipoConversao.Texto, TipoPadding.Esquerda, '0')]
        public int Id { get; set; } 

        [CobolColumn(2, 12, TipoConversao.Texto, TipoPadding.Esquerda, '0', TipoConversaoSaida.Decimal, 2)]
        public decimal Valor { get; set; } 

    } 

} 

