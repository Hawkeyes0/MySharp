using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net
{
    public abstract class Logger
    {
        public static string RootLoggerName { get; } = "Root";

        /// <summary>
        /// Name of this <code>Logger</code>
        /// </summary>
        public string Name { get; protected set; }

        public abstract bool IsTraceEnabled(Marker marker = null);

        public abstract void Trace(string msg, Exception exception = null, Marker marker = null);

        public abstract bool IsDebugEnabled(Marker marker = null);

        public abstract void Debug(string msg, Exception exception = null, Marker marker = null);

        public abstract bool IsInfoEnabled(Marker marker = null);

        public abstract void Info(string msg, Exception exception = null, Marker marker = null);

        public abstract bool IsWarnEnabled(Marker marker = null);

        public abstract void Warn(string msg, Exception exception = null, Marker marker = null);

        public abstract bool IsErrorEnabled(Marker marker = null);

        public abstract void Error(string msg, Exception exception = null, Marker marker = null);
    }
}
