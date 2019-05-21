using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Slf4net.Helpers;

namespace MySharp.Logging.Slf4net.Event
{
    public class SubstituteLoggingEvent : ILoggingEvent
    {
        public Level Level { get; set; }
        public Marker Marker { get; set; }
        public string LoggerName { get; set; }
        public string Message { get; set; }
        public string TreadName { get; set; }
        public object[] ArgumentArray { get; set; }
        public long TimeStamp { get; set; }
        public Exception Exception { get; set; }

        public SubstituteLogger Logger { get; set; }
    }
}
