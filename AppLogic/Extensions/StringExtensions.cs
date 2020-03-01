using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string str)
            => string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str) ? true : false;
    }
}
