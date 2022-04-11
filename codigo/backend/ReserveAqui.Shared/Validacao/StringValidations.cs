namespace ReserveAqui.Shared.Validacao
{
    public class StringValidations : BaseValidations<string, StringValidations>
    {
        public StringValidations(string value) : base(value)
        {
        }

        public StringValidations IsNotNullOrWhiteSpace(string message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                Error(message);

            return this;
        }

        public StringValidations IsNotEqualTo(string comp, string message = null)
        {
            if (value == comp)
                Error(message);

            return this;
        }

        public StringValidations HasLengthBetween(int min, int max, string message = null)
        {
            IsNotNull(message);
            
            if (value.Length < min || value.Length > max)
                Error(message);

            return this;
        }

        public StringValidations HasLengthEqual(int size, string message = null)
        {
            return HasLengthBetween(size, size, message);
        }

    }
}