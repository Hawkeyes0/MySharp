using System;
using MySharp.Logging.Slf4net.Helpers;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net
{
    public static class MarkerFactory
    {
        private static readonly IMarkerFactory Factory;

        static MarkerFactory()
        {
            SlfServiceProvider provider = LoggerFactory.GetProvider();
            if (provider == null)
            {
                Factory = new BasicMarkerFactory();
            }
            else
            {
                provider.Initialize();
                Factory = provider.MarkerFactory;
            }
        }

        public static Marker GetMarker(string name) => Factory.GetMarker(name);

        public static Marker GetDetachedMarker(string name) => Factory.GetDetachedMarker(name);

        public static IMarkerFactory GetMarkerFactory() => Factory;
    }
}