using System;

namespace ReserveAqui.Shared.Util
{
    public static class DateTimeUtil
    {
        public static DateTime ConverterDeTimestamp(double timestamp)
        {
            var dataHora = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dataHora = dataHora.AddSeconds(timestamp).ToLocalTime();
            return dataHora;
        }
    }
}
