using ReserveAqui.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace ReserveAqui.Shared.Validacao
{
    public static class Validate
    {
        public static IntValidations That(int? value)
        {
            return new IntValidations(value);
        }

        public static DoubleValidations That(double? value)
        {
            return new DoubleValidations(value);
        }

        public static StringValidations That(string value)
        {
            return new StringValidations(value);
        }

        public static ObjectValidations That(object value)
        {
            return new ObjectValidations(value);
        }

        public static ListValidations<T> That<T>(List<T> value)
        {
            return new ListValidations<T>(value);
        }

        public static DateTimeValidations That(DateTime? value)
        {
            return new DateTimeValidations(value);
        }

        public static BoolValidations That(bool value)
        {
            return new BoolValidations(value);
        }

        public static void ThrowError()
        {
            throw new ParametroInvalidoException();
        }

        public static void ThrowError(string message)
        {
            throw new ParametroInvalidoException(message);
        }
    }
}
