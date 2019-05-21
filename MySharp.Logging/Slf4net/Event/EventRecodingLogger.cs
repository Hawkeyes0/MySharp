using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MySharp.Logging.Slf4net.Helpers;

namespace MySharp.Logging.Slf4net.Event
{
    public class EventRecodingLogger : Logger
    {
        private SubstituteLogger _logger;
        private Queue<SubstituteLoggingEvent> _eventQueue;

        public EventRecodingLogger(SubstituteLogger logger, Queue<SubstituteLoggingEvent> eventQueue)
        {
            _logger = logger;
            _eventQueue = eventQueue;
            Name = logger.Name;
        }

        private void RecordEvent(Level level, string msg, Exception exception, Marker marker = null)
        {
            _eventQueue.Enqueue(new SubstituteLoggingEvent
            {
                TimeStamp = DateTime.Now.Ticks,
                Level = level,
                Logger = _logger,
                LoggerName = Name,
                Marker = marker,
                Message = msg,
                Exception = exception,
                TreadName = Thread.CurrentThread.Name
            });
        }

        public override bool IsTraceEnabled(Marker marker = null)
        {
            return true;
        }

        public override void Trace(string msg, Exception exception = null, Marker marker = null)
        {
            RecordEvent(Level.Trace, msg, exception, marker);
        }

        public override bool IsDebugEnabled(Marker marker = null)
        {
            return true;
        }

        public override void Debug(string msg, Exception exception = null, Marker marker = null)
        {
            RecordEvent(Level.Debug, msg, exception, marker);
        }

        public override bool IsInfoEnabled(Marker marker = null)
        {
            return true;
        }

        public override void Info(string msg, Exception exception = null, Marker marker = null)
        {
            RecordEvent(Level.Info, msg, exception, marker);
        }

        public override bool IsWarnEnabled(Marker marker = null)
        {
            return true;
        }

        public override void Warn(string msg, Exception exception = null, Marker marker = null)
        {
            RecordEvent(Level.Warn, msg, exception, marker);
        }

        public override bool IsErrorEnabled(Marker marker = null)
        {
            return true;
        }

        public override void Error(string msg, Exception exception = null, Marker marker = null)
        {
            RecordEvent(Level.Error, msg, exception, marker);
        }
    }
}
