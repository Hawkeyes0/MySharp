using System;

namespace MySharp.Logging.Slf4net.Event
{
    public interface ILoggingEvent
    {
        Level Level { get; }

        Marker Marker { get; }

        string LoggerName { get; }

        string Message { get; }

        string TreadName { get; }

        long TimeStamp { get; }

        Exception Exception { get; }
    }
}