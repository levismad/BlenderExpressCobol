using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Levismad.Framework.Annotations;

namespace Levismad.Framework
{

    public static class SaidaCobolExtension
    {
        public static T GetCobolModel<T>(this string dataRaw)
        {
            var data = dataRaw;
            var instance = Activator.CreateInstance<T>();
            var fimConversao = false;

            var listaPropriedades = new List<CobolColumn>();
            var properties = instance.GetType().GetProperties();
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
                if (fimConversao) return;
                try
                {
                    if (!csvColumnDef.IsGroupClass)
                    {
                        if (data.Length < csvColumnDef.Tamanho) fimConversao = true;

                        var propertie = csvColumnDef.Propriedade;
                        var typeConverter = TypeDescriptor.GetConverter(propertie.PropertyType);

                        var slice = data.Substring(0, csvColumnDef.Tamanho);
                        data = data.Substring(csvColumnDef.Tamanho);
                        if(csvColumnDef.ErrorCode && slice != "00000")
                        {
                            fimConversao = true;
                            var value = typeConverter.ConvertFromString(slice);
                            instance.SetPropertyValue(csvColumnDef.Propriedade.Name, value);

                            instance.SetPropertyValue("mensagemErro", data);

                        }

                        else
                        {
                            if (csvColumnDef.Conversao == TipoConversao.Data) csvColumnDef.ConversaoSaida = TipoConversaoSaida.Data;

                            switch (csvColumnDef.ConversaoSaida)
                            {
                                case TipoConversaoSaida.Data:
                                case TipoConversaoSaida.TimeStamp:
                                    slice =
                                        $"{slice.Substring(6, 2)}/{slice.Substring(4, 2)}/{slice.Substring(0, 4)}";
                                    var date = DateTime.ParseExact(slice, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    instance.SetPropertyValue(csvColumnDef.Propriedade.Name, date);
                                    break;
                                case TipoConversaoSaida.Decimal:
                                    slice =
                                        $"{slice.Substring(0, slice.Length - csvColumnDef.CasasDecimais)},{slice.Substring(slice.Length - csvColumnDef.CasasDecimais)}";
                                    var valueDecimal = typeConverter.ConvertFromString(slice);
                                    instance.SetPropertyValue(csvColumnDef.Propriedade.Name, valueDecimal);
                                    break;
                                default:
                                    var value = typeConverter.ConvertFromString(slice);
                                    instance.SetPropertyValue(csvColumnDef.Propriedade.Name, value);
                                    break;
                            }
                        }
                    }
                    else
                    {

                        var val = instance.GetPropertyValue<int>(csvColumnDef.RepeatColumn);
                        var listType = typeof(List<>);
                        var constructedListType = listType.MakeGenericType(csvColumnDef.GroupClass);
                        var aList = (IList)Activator.CreateInstance(constructedListType);

                        for (var i = 0; i < val; i++)
                        {
                            var instanceChild = Activator.CreateInstance(csvColumnDef.GroupClass);

                            var listaPropriedadesGroup = new List<CobolColumn>();
                            var propertiesGroup = csvColumnDef.GroupClass.GetProperties();
                            foreach (var propertie in propertiesGroup.Where(p => p.GetCustomAttributes(true).Any()))
                            {
                                var column = (CobolColumn)propertie.GetCustomAttributes(typeof(CobolColumn), false).FirstOrDefault();
                                if (column == null) continue;
                                column.Propriedade = propertie;
                                listaPropriedadesGroup.Add(column);
                            }
                            listaPropriedadesGroup = listaPropriedadesGroup.OrderBy(x => x.Posicao).ToList();
                            listaPropriedadesGroup.ForEach(column =>
                            {
                                var slice = data.Substring(0, column.Tamanho);
                                data = data.Substring(column.Tamanho);

                                var propertie = column.Propriedade;
                                var typeConverter = TypeDescriptor.GetConverter(propertie.PropertyType);

                                var sliceGroup = slice.Substring(0, column.Tamanho);

                                if (column.Conversao == TipoConversao.Data) column.ConversaoSaida = TipoConversaoSaida.Data;

                                switch (column.ConversaoSaida)
                                {
                                    case TipoConversaoSaida.Data:
                                    case TipoConversaoSaida.TimeStamp:
                                        sliceGroup =
                                            $"{sliceGroup.Substring(6, 2)}/{sliceGroup.Substring(4, 2)}/{sliceGroup.Substring(0, 4)}";
                                        var date = DateTime.ParseExact(sliceGroup, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        instanceChild.SetPropertyValue(column.Propriedade.Name, date);
                                        break;
                                    case TipoConversaoSaida.Decimal:
                                        sliceGroup =
                                            $"{sliceGroup.Substring(0, sliceGroup.Length - column.CasasDecimais)},{sliceGroup.Substring(sliceGroup.Length - column.CasasDecimais)}";
                                        var valueDecimal = typeConverter.ConvertFromString(sliceGroup);
                                        instanceChild.SetPropertyValue(column.Propriedade.Name, valueDecimal);
                                        break;
                                    default:
                                        var value = typeConverter.ConvertFromString(sliceGroup);
                                        instanceChild.SetPropertyValue(column.Propriedade.Name, value);
                                        break;
                                }

                            });

                            aList.Add(instanceChild);


                        }
                        instance.SetPropertyValue(csvColumnDef.Propriedade.Name, aList); 


                    }
                }
                catch (Exception)
                {
                    throw new FormatException("Não foi possivel converter os dados para a classe.");
                }
            });
            return instance;
        }
    }
}
