using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    public static class Util
    {
        public static void Report(string msg, Exception exception = null)
        {
            Console.WriteLine($"SLF4NET: {msg}");
            if (exception == null) return;

            Console.WriteLine("Reported Exception:");
            Console.WriteLine(exception.StackTrace);
        }
    }
}
