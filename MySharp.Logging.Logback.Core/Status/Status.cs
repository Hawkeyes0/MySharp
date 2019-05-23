using System;
using System.Collections.Generic;

namespace MySharp.Logging.Logback.Core.Status
{
    public interface IStatus
    {
        int Level { get; }

        int EffectiveLevel { get; }

        object Origin { get; }

        string Message { get; }

        Exception Exception { get; }

        DateTime Date { get; }

        bool HasChildren { get; }

        void Add(IStatus child);

        bool Remove(IStatus child);

        IEnumerator<IStatus> GetEnumerator();
    }

    public abstract class Status : IStatus
    {
        public const int Info = 0, Warn = 1, Error = 2;

        public abstract int Level { get; }
        public abstract int EffectiveLevel { get; }
        public abstract object Origin { get; }
        public abstract string Message { get; }
        public abstract Exception Exception { get; }
        public abstract DateTime Date { get; }
        public abstract bool HasChildren { get; }
        public abstract void Add(IStatus child);
        public abstract bool Remove(IStatus child);
        public abstract IEnumerator<IStatus> GetEnumerator();
    }
}