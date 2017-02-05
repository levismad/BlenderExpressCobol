namespace Levismad.Framework
{
    public static class NumberValidators
    {
        public static bool Validar(this decimal? number, bool validarZero, bool validarNegativo = false, bool validarNulo = false)
        {
            if (validarNulo && number == null) return false;
            if (!validarNulo && number == null) return true;

            var n = (decimal)number;
            return n.Validar<decimal>(validarZero, validarNegativo);
        }
        public static bool Validar(this int? number, bool validarZero, bool validarNegativo = false, bool validarNulo = false)
        {
            if (validarNulo && number == null) return false;
            if (!validarNulo && number == null) return true;
            var n = (int)number;
            return n.Validar<int>(validarZero, validarNegativo);
        }
        public static bool Validar(this long? number, bool validarZero, bool validarNegativo = false, bool validarNulo = false)
        {
            if (validarNulo && number == null) return false;
            if (!validarNulo && number == null) return true;
            var n = (int)number;
            return n.Validar<int>(validarZero, validarNegativo);
        }
        public static bool Validar(this long number, bool validarZero, bool validarNegativo = false)
        {
            return number.Validar<long>(validarZero, validarNegativo);
        }
        public static bool Validar(this decimal number, bool validarZero, bool validarNegativo = false)
        {
            return number.Validar<decimal>(validarZero, validarNegativo);
        }
        public static bool Validar(this int number, bool validarZero, bool validarNegativo = false)
        {
            return number.Validar<int>(validarZero, validarNegativo);
        }

        private static bool Validar<T>(this T number, bool validarZero, bool validarNegativo = false)
        {
            if (validarZero && decimal.Parse(number.ToString()) == 0)
                return false;
            return !validarNegativo || decimal.Parse(number.ToString()) >= 0;
        }
        public static decimal Nvl(this decimal? value)
        {
            var retorno = value ?? 0;
            return retorno;
        }
    }
}
