using MySharp.Logging.Slf4net.Event;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class SubstituteLogger : Logger
    {
        private volatile Logger _delegate;
        private readonly bool _createdPostInitialization;
        private EventRecodingLogger _eventRecodingLogger;
        private readonly Queue<SubstituteLoggingEvent> _eventQueue;
        private bool? _delegateEventAware;
        private MethodInfo _logMethodCache;

        public SubstituteLogger(string name, Queue<SubstituteLoggingEvent> eventQueue, bool createdPostInitialization)
        {
            Name = name;
            _createdPostInitialization = createdPostInitialization;
            _eventQueue = eventQueue;
        }

        Logger Delegate()
        {
            if (_delegate != null)
                return _delegate;
            if (_createdPostInitialization)
                return NopLogger.Instance;
            else
                return GetEventRecordingLogger();
        }

        Logger GetEventRecordingLogger()
        {
            return _eventRecodingLogger ?? (_eventRecodingLogger = new EventRecodingLogger(this, _eventQueue));
        }

        public override bool IsTraceEnabled(Marker marker = null)
        {
            return Delegate().IsTraceEnabled(marker);
        }

        public override void Trace(string msg, Exception exception = null, Marker marker = null)
        {
            Delegate().Trace(msg,exception,marker);
        }

        public override bool IsDebugEnabled(Marker marker = null)
        {
            return Delegate().IsDebugEnabled(marker);
        }

        public override void Debug(string msg, Exception exception = null, Marker marker = null)
        {
            Delegate().Debug(msg, exception, marker);
        }

        public override bool IsInfoEnabled(Marker marker = null)
        {
            return Delegate().IsInfoEnabled(marker);
        }

        public override void Info(string msg, Exception exception = null, Marker marker = null)
        {
            Delegate().Info(msg, exception, marker);
        }

        public override bool IsWarnEnabled(Marker marker = null)
        {
            return Delegate().IsWarnEnabled(marker);
        }

        public override void Warn(string msg, Exception exception = null, Marker marker = null)
        {
            Delegate().Warn(msg, exception, marker);
        }

        public override bool IsErrorEnabled(Marker marker = null)
        {
            return Delegate().IsErrorEnabled(marker);
        }

        public override void Error(string msg, Exception exception = null, Marker marker = null)
        {
            Delegate().Error(msg, exception, marker);
        }

        public void SetDelegate(Logger delegateLogger)
        {
            _delegate = delegateLogger;
        }

        public bool IsDelegateEventAware()
        {
            if (_delegateEventAware.HasValue)
                return _delegateEventAware.Value;

            try
            {
                _logMethodCache = _delegate.GetType().GetMethod("Log", new[] {typeof(ILoggingEvent)});
                _delegateEventAware = true;
            }
            catch (AmbiguousMatchException)
            {
                _delegateEventAware = true;
            }

            return _delegateEventAware.Value;
        }

        public void Log(ILoggingEvent loggingEvent)
        {
            if (IsDelegateEventAware())
            {
                try
                {
                    _logMethodCache.Invoke(_delegate, new object[] {loggingEvent});
                }
                catch
                {
                    // ignored
                }
            }
        }

        public bool IsDelegateNull() => _delegate == null;

        public bool IsDelegateNop() => _delegate is NopLogger;
    }
}
