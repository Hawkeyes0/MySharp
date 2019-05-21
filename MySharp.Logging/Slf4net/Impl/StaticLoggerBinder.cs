using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Impl
{
    class StaticLoggerBinder
    {
        public static StaticLoggerBinder Singleton { get; } = new StaticLoggerBinder();

        public static Version RequestApiVersion = Version.Parse("1.1.1");

        public static StaticLoggerBinder GetSingleton()
        {
            return Singleton;
        }

        internal ILoggerFactory GetLoggerFactory()
        {
            throw new NotSupportedException();
        }
    }
}
