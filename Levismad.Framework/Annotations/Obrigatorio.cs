using System;

namespace Levismad.Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Obrigatorio : Attribute
    {
        public bool IsNullable { get; private set; }
        public Obrigatorio(bool isNullable)
        {
            IsNullable = isNullable;
        }
    }
}
