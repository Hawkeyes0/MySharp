using System;

namespace MySharp.Logging.Logback.Core.Status
{
    public class InfoStatus : StatusBase
    {
        public InfoStatus(string msg, object origin, Exception ex = null) : base(Info, msg, origin, ex)
        {
        }
    }

    public class WarnStatus : StatusBase
    {
        public WarnStatus(string msg, object origin, Exception ex = null) : base(Info, msg, origin, ex)
        {
        }
    }

    public class ErrorStatus : StatusBase
    {
        public ErrorStatus(string msg, object origin, Exception ex = null) : base(Info, msg, origin, ex)
        {
        }
    }

}