using MySharp.Logging.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging
{
    public static class LoggerFactory
    {
        private static readonly Dictionary<string, Logger> Loggers = new Dictionary<string, Logger>();

        internal static Logger Root { get; }

        public static int Count { get; internal set; }

        static LoggerFactory()
        {
            Root = new Logger(Logger.RootLoggerName, null);
            Root.LogLevel = LogLevel.Debug;
            Loggers[Logger.RootLoggerName] = Root;
        }

        public static Logger GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        public static Logger GetLogger(string name)
        {
            if (name == null)
                throw new ArgumentException("cannot be null", nameof(name));

            if (Logger.RootLoggerName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return Root;

            int i = 0;
            Logger logger = Root;

            if (Loggers.ContainsKey(name))
                return Loggers[name];

            string childName;
            Logger childLogger;
            while (true)
            {
                int h = LoggerNameUtil.GetSeparatorIndexOf(name, i);
                childName = h == -1 ? name : name.Substring(0, h - i);

                i = h + 1;
                lock (logger)
                {
                    childLogger = logger.GetChildByName(childName);
                    if (childLogger == null)
                    {
                        childLogger = logger.CreateChildByName(childName);
                        Loggers[childName] = childLogger;
                        Count++;
                    }
                }
                logger = childLogger;
                if (h == -1)
                    return childLogger;
            }
        }
    }
}
