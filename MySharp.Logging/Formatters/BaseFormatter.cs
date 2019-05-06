using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Formatters
{
    public abstract class BaseFormatter
    {
        internal abstract string Format(string msg, LogLevel level, Exception exception);
    }
}
