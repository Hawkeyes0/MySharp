using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class NopLoggerFactory : ILoggerFactory
    {
        public Logger GetLogger(string name)
        {
            return NopLogger.Instance;
        }
    }
}
