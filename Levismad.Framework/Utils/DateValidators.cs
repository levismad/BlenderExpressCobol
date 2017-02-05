using System;

namespace Levismad.Framework
{
    public static class DateValidators
    {
        public static bool Validar(this DateTime data, DateTime? minValue = null, DateTime? maxValue = null)
        {
            if (data == DateTime.MinValue)
                return false;
            if (data == DateTime.MaxValue)
                return false;
            if (minValue != null && data < minValue)
                return false;
            return maxValue == null || !(data > maxValue);
        }
        public static bool Validar(this DateTime? data,bool validarNulo = false, DateTime? minValue = null, DateTime? maxValue = null)
        {
            if(validarNulo && data == null)
                return false;
            if (data == DateTime.MinValue)
                return false;
            if (data == DateTime.MaxValue)
                return false;
            if (minValue != null && data < minValue)
                return false;
            return maxValue == null || !(data > maxValue);
        }
    }
}
