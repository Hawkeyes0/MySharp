using System;
using System.Collections.Generic;
using System.Text;

namespace MySharp.Logging.Logback.Core.Status
{
    public abstract class StatusBase : Status
    {
        private static readonly List<IStatus> EmptyList = new List<IStatus>();

        private List<IStatus> _childrenList;

        public StatusBase(int level, string msg, object origin, Exception ex = null)
        {
            Level = level;
            Message = msg;
            Origin = origin;
            Exception = ex;
            Date = DateTime.Now;
        }

        public sealed override int Level { get; }

        public sealed override string Message { get; }

        public sealed override object Origin { get; }

        public sealed override Exception Exception { get; }

        public sealed override DateTime Date { get; }

        public override void Add(IStatus child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));
            if (_childrenList == null)
                _childrenList = new List<IStatus>();
            _childrenList.Add(child);
        }

        public override bool HasChildren => _childrenList?.Count > 0;

        public override IEnumerator<IStatus> GetEnumerator() => _childrenList?.GetEnumerator() ?? EmptyList.GetEnumerator();

        public override bool Remove(IStatus child)
        {
            if (_childrenList == null) return false;
            return _childrenList.Remove(child);
        }

        public override int EffectiveLevel
        {
            get
            {
                int result = Level;

                using (var it = GetEnumerator())
                {
                    while (it.MoveNext())
                    {
                        IStatus s = it.Current;
                        if (s == null) continue;
                        int effLevel = s.EffectiveLevel;
                        if (effLevel > result)
                            result = effLevel;
                    }
                }

                return result;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (EffectiveLevel)
            {
                case Info:
                    sb.Append("INFO");
                    break;
                case Warn:
                    sb.Append("WARN");
                    break;
                case Error:
                    sb.Append("ERROR");
                    break;
            }

            if (Origin != null)
            {
                sb.Append($" in {Origin} -");
            }

            sb.Append($" {Message}");

            if (Exception != null)
                sb.Append($" {Exception}");

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + Level;
            result = prime * result + Message?.GetHashCode() ?? 0;
            return result;
        }

        public override bool Equals(object obj)
        {
            StatusBase other = obj as StatusBase;
            if (other == null)
                return false;
            if (other == this)
                return true;

            if (other.Level != Level)
                return false;
            if (Message == null)
            {
                if (other.Message != null)
                    return false;
            }
            else if (Message != other.Message)
                return false;

            return true;
        }
    }
}