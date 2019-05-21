using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySharp.Logging.Slf4net.Event;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class SubstituteLoggerFactory : ILoggerFactory
    {
        private bool _postInitialization = false;

        readonly Dictionary<string, SubstituteLogger> _loggers = new Dictionary<string, SubstituteLogger>();

        private readonly Queue<SubstituteLoggingEvent> _events = new Queue<SubstituteLoggingEvent>();

        public Logger GetLogger(string name)
        {
            SubstituteLogger logger = _loggers[name] ?? (_loggers[name] = new SubstituteLogger(name, _events, _postInitialization));

            return logger;
        }

        public List<string> GetLoggerNames() => _loggers.Keys.ToList();

        public List<SubstituteLogger> GetLoggers() => _loggers.Values.ToList();

        public Queue<SubstituteLoggingEvent> EventQueue => _events;

        public void PostInitialization() => _postInitialization = true;

        public void Clear()
        {
            _loggers.Clear();
            _events.Clear();
        }
    }
}
