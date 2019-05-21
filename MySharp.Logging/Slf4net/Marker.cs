using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Slf4net
{
    public abstract class Marker
    {
        public const string AnyMarker = "*";

        public const string AnyNonNullMarker = "+";

        public string Name { get; protected set; }

        public abstract void Add(Marker marker);

        public abstract bool Remove(Marker marker);

        public abstract bool HasChildren();

        public abstract bool HasReferences();

        public abstract IEnumerator<Marker> GetEnumerator();

        public abstract bool Contains(Marker other);

        public abstract bool Contains(string name);

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();
    }
}
