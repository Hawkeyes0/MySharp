using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class NopLogger : MarkerIgnoringBase
    {
        public static NopLogger Instance { get; } = new NopLogger();

        private NopLogger()
        {
            Name = "NOP";
        }

        public override bool IsTraceEnabled(Marker marker = null)
        {
            return false;
        }

        public override void Trace(string msg, Exception exception = null, Marker marker = null)
        {
            ;
        }

        public override bool IsDebugEnabled(Marker marker = null)
        {
            return false;
        }

        public override void Debug(string msg, Exception exception = null, Marker marker = null)
        {
            ;
        }

        public override bool IsInfoEnabled(Marker marker = null)
        {
            return false;
        }

        public override void Info(string msg, Exception exception = null, Marker marker = null)
        {
            ;
        }

        public override bool IsWarnEnabled(Marker marker = null)
        {
            return false;
        }

        public override void Warn(string msg, Exception exception = null, Marker marker = null)
        {
            ;
        }

        public override bool IsErrorEnabled(Marker marker = null)
        {
            return false;
        }

        public override void Error(string msg, Exception exception = null, Marker marker = null)
        {
            ;
        }
    }
}
