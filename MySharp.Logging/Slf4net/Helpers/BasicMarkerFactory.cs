using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class BasicMarkerFactory : IMarkerFactory
    {
        private readonly Dictionary<string, Marker> markers = new Dictionary<string, Marker>();

        public Marker GetMarker(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Marker marker = markers[name];
            if (marker == null)
            {
                marker = new BasicMarker(name);
                markers[name] = marker;
            }

            return marker;
        }

        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        public bool DetachMarker(string name)
        {
            throw new NotImplementedException();
        }

        public Marker GetDetachedMarker(string name)
        {
            throw new NotImplementedException();
        }
    }
}
