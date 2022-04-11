namespace ReserveAqui.Shared.Validacao
{

    public class DoubleValidations : BaseValidations<double?, DoubleValidations>
    {
        public DoubleValidations(double? value) : base(value)
        {
        }

        public DoubleValidations IsGreaterThan(double otherValue, string message = null)
        {
            if (value == null || value <= otherValue)
                Error(message);

            return this;
        }

        public DoubleValidations IsGreaterThanOrEqualTo(double otherValue, string message = null)
        {
            if (value == null || value < otherValue)
                Error(message);

            return this;
        }

        public DoubleValidations IsLowerThan(double otherValue, string message = null)
        {
            if (value == null || value >= otherValue)
                Error(message);

            return this;
        }

        public DoubleValidations IsLowerThanOrEqualTo(double otherValue, string message = null)
        {
            if (value == null || value > otherValue)
                Error(message);

            return this;
        }

        public DoubleValidations IsNotNegative(string message = null)
        {
            return IsGreaterThanOrEqualTo(0, message);
        }

        public DoubleValidations IsEqualTo(double otherValue, string message = null)
        {
            if (value == null || value != otherValue)
                Error(message);

            return this;
        }
    }
}
