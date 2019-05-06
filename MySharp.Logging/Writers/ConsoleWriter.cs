using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Writers
{
    public class ConsoleWriter : BaseWriter
    {
        public override void Write(string msg, LogLevel level, Exception exception)
        {
            string str = Formatter.Format(msg, level, exception);
            Console.WriteLine(str);
        }
    }
}
