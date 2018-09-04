using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain
{
    public static class StringExtensions
    {
        public static string ToCostString(this decimal number)
        {
            return number.ToString("##,###");
        }

        public static string ToCostString(this int number)
        {
            return number.ToString("##,###");
        }
    }
}
