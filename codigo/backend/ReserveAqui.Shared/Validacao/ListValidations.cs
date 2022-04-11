using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Shared.Validacao
{
    public class ListValidations<T> : BaseValidations<List<T>, ListValidations<T>>
    {
        public ListValidations(List<T> value) : base(value)
        {
        }

        public ListValidations<T> IsNotNullOrEmpty(string message = null)
        {
            if (value == null || !value.Any())
                Error(message);

            return this;
        }

        public ListValidations<T> ShouldContain(T otherValue, string message = null)
        {
            if (value == null || !value.Any() || !value.Contains(otherValue))
                Error(message);
            
            return this;
        }

        public ListValidations<T> HasCountBetweenThan(int min, int max, string message = null)
        {
            if (value == null || value.Count < min || value.Count > max)
                Error(message);

            return this;
        }
    }
}