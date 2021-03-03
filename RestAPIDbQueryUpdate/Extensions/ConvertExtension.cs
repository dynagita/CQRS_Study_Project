using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Extensions
{
    public static class ConvertExtension
    {
        public static long ToLong(this string value)
        {
            try
            {
                long longout;
                long.TryParse(value, out longout);
                return longout;
            }
            catch
            {
                return 0;
            }
        }
    }
}
