using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Levismad.Framework.Annotations;

namespace Levismad.Framework
{
    public abstract class CobolModel
    {
        public string MensagemErro { get; set; }

        public override string ToString()
        {
            var listaPropriedades = new List<CobolColumn>();
            var objeto = this;

            var finalString = "";
            var properties = objeto.GetType().GetProperties();
            foreach (var propertie in properties.Where(p => p.GetCustomAttributes(true).Any()))
            {
                var csvColumnDef = (CobolColumn)propertie.GetCustomAttributes(typeof(CobolColumn), false).FirstOrDefault();
                if (csvColumnDef == null) continue;
                csvColumnDef.Propriedade = propertie;
                listaPropriedades.Add(csvColumnDef);
            }
            listaPropriedades = listaPropriedades.OrderBy(x => x.Posicao).ToList();
            listaPropriedades.ForEach(csvColumnDef =>
            {
                var propertie = csvColumnDef.Propriedade;
                var columnValue = "";
                try
                {
                    var val = propertie.GetValue(objeto, null);
                    //if (val != null)
                    //{
                    switch (csvColumnDef.Conversao)
                    {
                        case TipoConversao.Data:
                            columnValue = (val != null) ? ((DateTime)val).ToString("yyyyMMdd") : "";
                            break;
                        case TipoConversao.TimeStamp:
                            columnValue = (val != null) ? ((DateTime)val).ToString("yyyyMMddhhmmss") : "";
                            break;
                        case TipoConversao.Decimal:
                            var convert = decimal.Parse(val.ToString());
                            var inteiro = (int)convert;
                            var casas = convert - inteiro;
                            var tamanhoInt = csvColumnDef.Tamanho - csvColumnDef.CasasDecimais;
                            var casasMod = casas.ToString(CultureInfo.InvariantCulture).Length > 2 ? casas.ToString(CultureInfo.InvariantCulture).Substring(2) : casas.ToString(CultureInfo.InvariantCulture);
                            columnValue = inteiro.ToString().PadLeft(tamanhoInt, '0') + casasMod.ToString().PadRight(csvColumnDef.CasasDecimais, '0');
                            break;
                        default:
                            columnValue = val?.ToString() ?? "";
                            break;
                    }
                    if (columnValue.Length < csvColumnDef.Tamanho)
                    {
                        switch (csvColumnDef.Padding)
                        {
                            case TipoPadding.Esquerda:
                                columnValue = columnValue.PadLeft(csvColumnDef.Tamanho, csvColumnDef.PaddingCharacter);
                                break;
                            case TipoPadding.Direita:
                                columnValue = columnValue.PadRight(csvColumnDef.Tamanho, csvColumnDef.PaddingCharacter);
                                break;
                        }
                    }

                    //}
                    if (columnValue.Length > csvColumnDef.Tamanho)
                    {
                        columnValue = columnValue.Substring(0, csvColumnDef.Tamanho);
                    }


                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    finalString += columnValue;
                }
            });
            return finalString;
        }


    }
}
