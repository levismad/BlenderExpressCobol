using System;
using System.Collections.Generic;
using System.Globalization;
using Levismad.Dominios.Entrada;
using Levismad.Framework;
using Levismad.Framework.Contratos;

namespace Levismad.Repositorio.Regras
{
    public class Regra99Validator : IRegrasValidacao
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly AbstractRepository _repositorio;
        public Regra99Validator(AbstractRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public TipoDocumento GetDoc(object x)
        {
            if (x == null) return TipoDocumento.Cpf;
            return (TipoDocumento)x;
        }
        public void Validar(ref EntradaServico2 entrada)
        {
            var ultimoDoc = TipoDocumento.Cpf;
            try
            {

                /*
                 * Dados de cpf/cnpj retirados de : http://www.geradorcpf.com/ || http://www.geradorcnpj.com/
                 */
                var items = new List<ValidatorHelperClass<string>>() {
                    new ValidatorHelperClass<string>() { Value = "085.277.549-08", Required = true , CustomValidation = TipoDocumento.Cpf },
                    new ValidatorHelperClass<string>() { Value = "51.434.334/0001-04", Required = false , CustomValidation = TipoDocumento.Cgc},
                    new ValidatorHelperClass<string>() { Value = "65.117.632/0001-85", Required = false},
                    new ValidatorHelperClass<string>() { Value = "169.762.884-22", Required = false},
                    new ValidatorHelperClass<string>() { Value = null, Required = false},
                    new ValidatorHelperClass<string>() { Value = "", Required = false},
                };
                items.ForEach(x => {
                    ultimoDoc = GetDoc(x.CustomValidation);
                    if (!Validar(x)) throw new Exception();
                });
            }
            catch (Exception)
            {
                if(ultimoDoc == TipoDocumento.Cpf) throw new ValidacaoException("CPF Invalido", 99);
                throw new ValidacaoException("Cgc Invalido", 99);

            }
        }
        private static readonly List<string> CpfInvalidos = new List<string>()
        {
            "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999"
        };
        private static bool Validar(ValidatorHelperClass<string> item)
        {
            if (!item.IsValidClass())
            {
                return false;
            }
            if (string.IsNullOrEmpty(item.Value)) return true;

            var tipo = (TipoDocumento?)item.CustomValidation;
            var doc = item.Value;
            switch (tipo)
            {
                case null:
                    doc = doc.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
                    return doc.Length < 12 ? ValidarCpf(doc) : ValidarCnpj(doc);
                case TipoDocumento.Cpf:
                    return ValidarCpf(doc);
                case TipoDocumento.Cgc:
                    return ValidarCnpj(doc);
            }
            return true;
        }
        public static bool ValidarCnpj(string cnpj)
        {
            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace(",", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cnpj.EndsWith(digito);
        }
        public static bool ValidarCpf(string cpf)
        {
            var mt1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace(",","").Replace("-", "");

            if (CpfInvalidos.IndexOf(cpf) >= 0) return false;

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }

        public string GetValue<T>(T item)
        {
            if (item == null) return "";
            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                return item.ToString().Substring(0, 10);
            }
            if(typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
            {
                return Math.Truncate(decimal.Parse(item.ToString())).ToString(CultureInfo.InvariantCulture);
            }
            return item.ToString();
        }
    }
}