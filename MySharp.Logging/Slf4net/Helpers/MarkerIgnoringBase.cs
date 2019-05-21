using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    public abstract class MarkerIgnoringBase : NamedLoggerBase
    {
        public override string ToString()
        {
            return $"{GetType().Name}({Name})";
        }
    }
}
