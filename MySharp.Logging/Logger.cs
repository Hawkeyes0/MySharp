using System;
using System.Collections.Generic;
using System.Linq;
using MySharp.Logging.Util;
using MySharp.Logging.Writers;

namespace MySharp.Logging
{
    public class Logger
    {
        private event Action<string, LogLevel, Exception> WriteLog;

        public const string RootLoggerName = "Root";

        /// <summary>
        /// Full qualified class name
        /// </summary>
        public static string FullName { get; } = typeof(Logger).FullName;

        public List<BaseWriter> Writers { get; internal set; }

        private LogLevel effectiveLevel;

        public LogLevel LogLevel
        {
            get
            {
                return effectiveLevel;
            }
            set
            {
                if (effectiveLevel == value)
                    return;
                if (Name == RootLoggerName)
                    throw new ArgumentException("The level of root logger cannot be set.");

                effectiveLevel = value;

                foreach (Logger c in Children)
                {
                    c.LogLevel = value;
                }
            }
        }

        public Logger Parent { get; internal set; }

        public List<Logger> Children { get; internal set; }

        public string Name { get; internal set; }

        internal Logger(string name, Logger parent)
        {
            Name = name;
            Parent = parent;
        }

        public void Debug(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Debug);
        }

        internal Logger GetChildByName(string childName)
        {
            if (Children == null)
                return null;
            return Children.FirstOrDefault(l => l.Name == childName);
        }

        public void Info(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Info);
        }

        internal Logger CreateChildByName(string childName)
        {
            int i = LoggerNameUtil.GetSeparatorIndexOf(childName, this.Name.Length + 1);
            if (i != -1)
            {
                throw new ArgumentException($"For logger [{Name}] child name [{childName}] passed as parameter, may not includ '.' after index {Name.Length + 1}");
            }

            if (Children == null)
                Children = new List<Logger>();

            Logger childLogger = new Logger(childName, this)
            {
                effectiveLevel = LogLevel
            };
            Children.Add(childLogger);
            return childLogger;
        }

        public void Warning(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Warning);
        }

        public void Trace(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Trace);
        }

        public void Error(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Error);
        }

        public void Write(string msg, LogLevel level, Exception exception = null)
        {
            if (level <= LogLevel)
                WriteLog?.Invoke(msg, level, exception);
        }
    }
}
