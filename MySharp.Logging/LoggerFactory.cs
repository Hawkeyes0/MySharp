using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging
{
    public static class LoggerFactory
    {
        private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();


        public static Logger GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        public static Logger GetLogger(string name)
        {
            Logger logger;
            if (Loggers.ContainsKey(name))
                return Loggers[name];
            logger = new Logger(name);
        }
    }
}
