using System;
using System.Reflection;

namespace Levismad.Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CobolColumn : Attribute
    {
        public PropertyInfo Propriedade { get; set; }

        public int Posicao { get; private set; }
        public int Tamanho { get; private set; }
        public TipoConversao Conversao { get; private set; }
        public TipoPadding Padding { get; private set; }
        public char PaddingCharacter { get; private set; }
        public TipoConversaoSaida ConversaoSaida { get; set; }
        public int CasasDecimais { get; private set; }
        public bool IsGroupClass => GroupClass != null;
        public Type GroupClass { get; }
        public string RepeatColumn { get; set; }
        public bool ErrorCode { get; set; }


        public CobolColumn(int posicao, Type groupClass, string repeatColumn, bool errorCode = false)
        {
            Posicao = posicao;
            GroupClass = groupClass;
            RepeatColumn = repeatColumn;
            ErrorCode = errorCode;
        }

        public CobolColumn(int posicao, int tamanho, TipoPadding padding, char paddingCharacter, TipoConversao conversao = TipoConversao.Texto, bool errorCode = false)
        {
            Conversao = conversao;
            Posicao = posicao;
            Tamanho = tamanho;
            Padding = padding;
            PaddingCharacter = paddingCharacter;
            ErrorCode = errorCode;
        }
        public CobolColumn(int posicao, int tamanho, TipoConversao conversao = TipoConversao.Texto, TipoPadding padding = TipoPadding.SemPadding, char paddingCharacter = ' ',TipoConversaoSaida conversaoSaida = TipoConversaoSaida.Texto, int casasDecimais = 0, bool errorCode = false)
        {
            Conversao = conversao;
            Posicao = posicao;
            Tamanho = tamanho;
            Padding = padding;
            PaddingCharacter = paddingCharacter;
            ConversaoSaida = conversaoSaida;
            CasasDecimais = casasDecimais;
            ErrorCode = errorCode;
        }
    }
}