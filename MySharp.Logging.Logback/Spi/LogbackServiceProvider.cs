using System;
using MySharp.Logging.Logback.Core;
using MySharp.Logging.Slf4net;
using MySharp.Logging.Slf4net.Helpers;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Logback.Classic.Spi
{
    public class LogbackServiceProvider : SlfServiceProvider
    {
        public ILoggerFactory LoggerFactory => _defaultLoggerContext;
        public IMarkerFactory MarkerFactory { get; private set; }
        public MDCAdapter MdcAdapter { get; private set; }
        public Version RequestedApiVersion { get; } = Version.Parse("1.0.0");

        private LoggerContext _defaultLoggerContext;
        public void Initialize()
        {
            _defaultLoggerContext = new LoggerContext();
            _defaultLoggerContext.Name = CoreConstants.DefaultContextName;
            InitializeLoggerContext();
            MarkerFactory = new BasicMarkerFactory();
            MdcAdapter = new LogbackMDCAdapter();
        }

        private void InitializeLoggerContext()
        {
            throw new NotImplementedException();
        }
    }
}
