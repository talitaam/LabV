namespace ReserveAqui.Shared.Validacao
{
    public class IntValidations : BaseValidations<int?, IntValidations>
    {
        public IntValidations(int? value) : base(value)
        {
        }

        public IntValidations IsGreaterThan(int otherValue, string message = null)
        {
            if (value == null || value <= otherValue)
                Error(message);

            return this;
        }

        public IntValidations IsGreaterThanOrEqualTo(int otherValue, string message = null)
        {
            if (value == null || value < otherValue)
                Error(message);

            return this;
        }

        public IntValidations IsLowerThan(int otherValue, string message = null)
        {
            if (value == null || value >= otherValue)
                Error(message);

            return this;
        }

        public IntValidations IsLowerThanOrEqualTo(int otherValue, string message = null)
        {
            if (value == null || value > otherValue)
                Error(message);

            return this;
        }

        public IntValidations IsNotNegative(string message = null)
        {
            return IsGreaterThanOrEqualTo(0, message);
        }

        public IntValidations IsEqualTo(int otherValue, string message = null)
        {
            if (value == null || value != otherValue)
                Error(message);

            return this;
        }
    }
}