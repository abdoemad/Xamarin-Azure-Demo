using System;
using System.Collections.Generic;
using System.Text;

namespace EShope.Helpers
{
    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var min = Math.Min(str.Length, maxLength);

            return str.Substring(0, min) + (str.Length <= maxLength ? string.Empty : "...");
        }

    }
}
