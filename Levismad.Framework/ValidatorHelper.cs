
namespace Levismad.Framework
{
    public class ValidatorHelper<T> where T : struct
    {
        public T? Value { get; set; }
        public bool Required { get; set; }
        public object CustomValidation { get; set; }
    }

    public class ValidatorHelperClass<T> where T : class
    {
        public T Value { get; set; }
        public bool Required { get; set; }
        public object CustomValidation { get; set; }
    }
    public static class ValidorHelper
    {
        public static bool IsValid<T>(this ValidatorHelper<T> item) where T : struct
        {
            return !item.Required || item.Value != null;
        }

        public static bool IsValidClass<T>(this ValidatorHelperClass<T> item) where T : class
        {
            if (item.Required && item.Value == null)
            {
                return false;
            }
            return typeof(T) != typeof(string) || (!item.Required || (!string.IsNullOrEmpty(item.Value?.ToString())));
        }
    }
}
