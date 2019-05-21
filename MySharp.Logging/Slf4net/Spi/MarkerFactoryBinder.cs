using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Spi
{
    public interface MarkerFactoryBinder
    {
        IMarkerFactory GetMarkerFactory();

        string GetMarkerFactoryClassStr();
    }
}
