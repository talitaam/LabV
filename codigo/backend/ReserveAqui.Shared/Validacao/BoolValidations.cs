namespace ReserveAqui.Shared.Validacao
{
    public class BoolValidations : BaseValidations<bool, BoolValidations>
    {
        public BoolValidations(bool value) : base(value)
        {
        }

        public BoolValidations IsTrue(string message = null)
        {
            if (!value)
                Error(message);

            return this;
        }

        public BoolValidations IsFalse(string message = null)
        {
            if (value)
                Error(message);

            return this;
        }
    }
}