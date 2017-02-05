using Levismad.Framework;
using Levismad.Framework.Annotations;

namespace Levismad.Dominios.Mainframe
{
    public class EntradaCobol : CobolModel
    {
        public EntradaCobol(string banco, string usuario, string senha)
        {
            CodigoDaFuncao = "001";
            UsuarioDoBancoDeDados = usuario;
            SenhaDoBancoDeDados = senha;
            NomeDoBancoDeDados = banco;

        }


        [CobolColumn(1, 3, TipoConversao.Texto, TipoPadding.Esquerda)]
        public string CodigoDaFuncao { get; set; }


        [CobolColumn(2, 10, TipoConversao.Texto, TipoPadding.Direita)]
        public string UsuarioDoBancoDeDados { get; set; }


        [CobolColumn(3, 10, TipoConversao.Texto, TipoPadding.Direita)]
        public string SenhaDoBancoDeDados { get; set; }


        [CobolColumn(4, 10, TipoConversao.Texto, TipoPadding.Direita)]
        public string NomeDoBancoDeDados { get; set; }
        
        


    }
}
