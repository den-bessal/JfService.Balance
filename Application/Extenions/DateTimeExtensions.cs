using System;

namespace JfService.Balance.Application.Extenions
{
    public static class DateTimeExtensions
    {
        public static int Quarter(this DateTime dt)
        {
            return dt.Month >= 1 && dt.Month <= 3
                ? 1
                : dt.Month >= 4 && dt.Month <= 6
                    ? 2
                    : dt.Month >= 7 && dt.Month <= 9
                        ? 3
                        : 4;
        }

        public static int MonthDiff(this DateTime dt1, DateTime dt2)
        {
            return ((dt1.Year - dt2.Year) * 12) + dt1.Month - dt2.Month;
        }
    }
}