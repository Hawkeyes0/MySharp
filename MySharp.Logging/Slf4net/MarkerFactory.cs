using System;
using MySharp.Logging.Slf4net.Helpers;
using MySharp.Logging.Slf4net.Impl;

namespace MySharp.Logging.Slf4net
{
    public static class MarkerFactory
    {
        private static IMarkerFactory _markerFactory;

        private static IMarkerFactory GetMarkerFactoryFromBinder()
        {
            return StaticMarkerBinder.Singleton.GetMarkerFactory();
        }

        static MarkerFactory()
        {
            try
            {
                _markerFactory = GetMarkerFactoryFromBinder();
            }
            catch (MissingMethodException)
            {
                _markerFactory = new BasicMarkerFactory();
            }
            catch (Exception e)
            {
                Util.Report("Unexpected failure while binding MarkerFactory");
            }
        }

        public static Marker GetMarker(string name) => _markerFactory.GetMarker(name);

        public static Marker GetDetachedMarker(string name) => _markerFactory.GetDetachedMarker(name);

        public static IMarkerFactory GetMarkerFactory() => _markerFactory;
    }
}