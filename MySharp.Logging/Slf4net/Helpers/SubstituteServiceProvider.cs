using System;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class SubstituteServiceProvider : SlfServiceProvider
    {
        public ILoggerFactory LoggerFactory { get; } = new SubstituteLoggerFactory();

        public IMarkerFactory MarkerFactory { get; } = new BasicMarkerFactory();

        public MDCAdapter MdcAdapter { get; } = new BasicMDCAdapter();

        public Version RequestedApiVersion => throw new NotSupportedException();

        public void Initialize()
        {
        }
    }
}
