using System;

namespace ReserveAqui.Shared.Validacao
{
    public class DateTimeValidations : BaseValidations<DateTime?, DateTimeValidations>
    {
        public DateTimeValidations(DateTime? value) : base(value) { }

        public DateTimeValidations IsNotNullOrMinValue(string message = null)
        {
            if (value == null || value == DateTime.MinValue)
                Error(message);

            return this;
        }

        public DateTimeValidations IsGreaterThan(DateTime? otherValue, string message = null)
        {
            if (value == null || otherValue == null ||
                DateTime.Compare(value.Value, otherValue.Value) < 0)
                Error(message);

            return this;
        }

        public DateTimeValidations IsGreaterThanOrEqualTo(DateTime? otherValue, string message = null)
        {
            if (value == null || value < otherValue)
                Error(message);

            return this;
        }

        public DateTimeValidations IsLowerThanOrEqualTo(DateTime? otherValue, string message = null)
        {
            if (value == null || value > otherValue)
                Error(message);

            return this;
        }

        public DateTimeValidations IsMaxDaysAfter(DateTime? otherValue, int maxDays)
        {
            if (value == null || otherValue == null)
                Error();

            var period = ((DateTime)value).Subtract((DateTime)otherValue);

            if (period.Days > maxDays)
                Error();

            return this;
        }
    }
}
