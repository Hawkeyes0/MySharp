using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Formatters;

namespace MySharp.Logging.Writers
{
    public abstract class BaseWriter
    {
        public BaseFormatter Formatter { get; set; }

        public abstract void Write(string msg, LogLevel level, Exception exception);
    }
}
