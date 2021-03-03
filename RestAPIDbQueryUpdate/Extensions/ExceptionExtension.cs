using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Extensions
{
    public static class ExceptionExtension
    {
        public static string AllMessages(this Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex.Message;
            }

            return $"{ex.Message}{Environment.NewLine}{ex.InnerException.AllMessages()}";
        }
    }
}
