using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySharp.Logging.Slf4net.Helpers
{
    internal class BasicMarker : Marker
    {
        private List<Marker> _referenceList = new List<Marker>();
        private const char Open = '[';
        private const char Close = ']';
        private const string Sep = ", ";

        public BasicMarker(string name)
        {
            Name = name;
        }

        public override void Add(Marker marker)
        {
            if (marker == null)
                throw new ArgumentNullException(nameof(marker));

            if (Contains(marker) || marker.Contains(this))
                return;
            _referenceList.Add(marker);
        }

        public override bool Remove(Marker marker) => _referenceList.Remove(marker);

        public override bool HasChildren() => HasReferences();

        public override bool HasReferences() => _referenceList.Any();

        public override IEnumerator<Marker> GetEnumerator() => _referenceList.GetEnumerator();

        public override bool Contains(Marker other)
        {
            if(other==null)
                throw new ArgumentNullException(nameof(other));

            if (Equals(other))
                return true;

            if (HasReferences())
            {
                foreach (Marker r in _referenceList)
                {
                    if (r.Contains(other))
                        return true;
                }
            }

            return false;
        }

        public override bool Contains(string name)
        {
            if(name==null)
                throw new ArgumentNullException(nameof(name));

            if (Name == name) return true;

            if (HasReferences())
            {
                foreach (Marker r in _referenceList)
                {
                    if (r.Contains(name)) return true;
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            Marker other = obj as Marker;
            if (other == null) return false;

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            if (!HasReferences())
                return Name;

            StringBuilder sb = new StringBuilder(Name);
            sb.Append(' ').Append(Open);
            foreach (Marker r in _referenceList)
            {
                sb.Append(r.Name).Append(Sep);
            }

            sb.Remove(sb.Length - Sep.Length, Sep.Length);
            sb.Append(Close);
            return sb.ToString();
        }
    }
}