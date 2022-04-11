namespace ReserveAqui.Shared.Validacao
{
    public class BaseValidations<T, C> where C : BaseValidations<T, C>
    {
        protected readonly T value;

        public BaseValidations(T value)
        {
            this.value = value;
        }

        public C IsNull(string message = null)
        {
            if (value != null)
                Error(message);

            return (C)this;
        }

        public C IsNotNull(string message = null)
        {
            if (value == null)
                Error(message);

            return (C)this;
        }

        public C IsEqualTo(T otherValue, string message = null)
        {
            if (value == null || !value.Equals(otherValue))
                Error(message);

            return (C)this;
        }

        protected void Error(string message = null)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Validate.ThrowError(message);
            else 
                Validate.ThrowError();
        }   
    }
}
