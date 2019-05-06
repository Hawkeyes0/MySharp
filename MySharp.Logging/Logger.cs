using System;
using System.Collections.Generic;
using MySharp.Logging.Writers;

namespace MySharp.Logging
{
    public class Logger
    {
        private event Action<string, LogLevel, Exception> WriteLog;

        public const string RootLoggerName = "Root";

        public List<BaseWriter> Writers { get; }

        public LogLevel LogLevel { get; }

        internal Logger(List<BaseWriter> writers)
        {
            Writers = writers;
            foreach (BaseWriter writer in writers)
            {
                WriteLog += writer.Write;
            }
        }

        public void Debug(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Debug);
        }

        public void Info(string msg, Exception exception = null)
        {
            Write(msg, LogLevel.Info);
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
            if(level <= LogLevel)
                WriteLog?.Invoke(msg, level, exception);
        }
    }
}
