using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net.Impl
{
    public class StaticMarkerBinder : MarkerFactoryBinder
    {
        public static StaticMarkerBinder Singleton { get; } = new StaticMarkerBinder();

        public IMarkerFactory GetMarkerFactory()
        {
            throw new NotImplementedException();
        }

        public string GetMarkerFactoryClassStr()
        {
            throw new NotImplementedException();
        }
    }
}
