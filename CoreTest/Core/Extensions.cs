using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Core
{
    public static class Extensions
    {
        public static string Format(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }
    }
}
