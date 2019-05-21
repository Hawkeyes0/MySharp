using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Slf4net;

namespace MySharp.Logging.Slf4net.Helpers
{
    public abstract class NamedLoggerBase : Logger
    {
        protected object ReadResolve()
        {
            return LoggerFactory.GetLogger(Name);
        }
    }
}
