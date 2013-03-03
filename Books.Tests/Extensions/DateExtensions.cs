using System;

namespace Books.Tests.Extensions
{
    public static class DateExtensions
    {
         public static DateTime ToDate(this string date)
         {
             return DateTime.Parse(date);
         }
    }
}