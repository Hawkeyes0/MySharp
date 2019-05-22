using System;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class NopServiceProvider:SlfServiceProvider
    {
        public SubstituteLoggerFactory LoggerFactory { get; } = new NopLoggerFactory();
        public IMarkerFactory MarkerFactory { get; } = new BasicMarkerFactory();
        public MDCAdapter MdcAdapter { get; } = new NopMDCAdapter();
        public Version RequestedApiVersion { get; } = Version.Parse("1.0.0");
        public void Initialize()
        {
        }
    }
}