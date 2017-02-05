using System;
using System.Collections.Generic;
using System.Linq;
using Levismad.Framework.Annotations;

namespace Levismad.Framework
{
    public static class ValidationUtil
    {
        public class ValidatorResult
        {
            public ValidatorResult()
            {
                IsValid = true;
                Erros = new List<ValidatorModel>();
            }
            public bool IsValid { get; set; }
            public List<ValidatorModel> Erros { get; set; }
        }
        public class ValidatorModel
        {
            public string Campo { get; set; }
            public string Mensagem { get; set; }
        }
        public static ValidatorResult ValidarObjeto(this object objeto)
        {
            var result = new ValidatorResult();
            var properties = objeto.GetType().GetProperties();
            var erros = new List<ValidatorModel>();
            foreach (var propertie in properties.Where(p => p.GetCustomAttributes(true).Any()))
            {
                var csvColumnDef = (Obrigatorio)propertie.GetCustomAttributes(typeof(Obrigatorio), false).First();
                try
                {
                    var val = propertie.GetValue(objeto, null);
                    
                    if ((val != null && string.IsNullOrEmpty(val.ToString())) != csvColumnDef.IsNullable)
                    {
                        erros.Add(new ValidatorModel()
                        {
                            Campo = propertie.Name,
                            Mensagem = "Campo não pode ser nullo"
                        });
                    }
                }
                catch (Exception)
                {
                    result.IsValid = false;
                }
            }
            if (!erros.Any()) return result;
            result.IsValid = false;
            result.Erros = erros;
            return result;
        }
        public static ValidatorResult ValidarCobol(this CobolModel objeto)
        {
            var result = new ValidatorResult();
            var erros = new List<ValidatorModel>();
            var properties = objeto.GetType().GetProperties();
            foreach (var propertie in from propertie in properties.Where(p => p.GetCustomAttributes(true).Any()) let csvColumnDef = (CobolColumn)propertie.GetCustomAttributes(typeof(CobolColumn), false).First() select propertie)
            {
                try
                {
                    var val = propertie.GetValue(objeto, null);

                    if ((string.IsNullOrEmpty(val?.ToString())))
                    {
                        erros.Add(new ValidatorModel()
                        {
                            Campo = propertie.Name,
                            Mensagem = "Campo não pode ser nullo"
                        });

                    }
                }
                catch (Exception)
                {
                    result.IsValid = false;
                }
            }
            if (!erros.Any()) return result;
            result.IsValid = false;
            result.Erros = erros;
            return result;
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                throw new ArgumentException("Property " + propertyName + " not found!");

            return (T)Convert.ChangeType(propertyInfo.GetValue(obj, null), typeof(T));
        }
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
                throw new ArgumentException("Property " + propertyName + " not found!");

            propertyInfo.SetValue(obj, value, null);
        }
    }
}
