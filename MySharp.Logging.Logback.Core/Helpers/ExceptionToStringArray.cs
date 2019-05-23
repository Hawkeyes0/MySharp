using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MySharp.Logging.Logback.Core.Helpers
{
    public class ExceptionToStringArray
    {
        internal static string[] Convert(Exception ex)
        {
            List<string> strList = new List<string>();
            Extract(strList, ex, null);
            return strList.ToArray();
        }

        private static void Extract(List<string> strList, Exception exception, object o)
        {
            throw new NotImplementedException();
        }
    }
}