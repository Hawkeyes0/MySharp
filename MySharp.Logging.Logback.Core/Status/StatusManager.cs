using System.Collections.Generic;

namespace MySharp.Logging.Logback.Core.Status
{
    public interface IStatusManager
    {
        void Add(IStatus status);

        List<IStatus> GetCopyOfStatuses();

        int Count { get; }

        bool Add(IStatusListener listener);

        void Remove(IStatusListener listener);

        void Clear();

        List<IStatusListener> GetCopyOfStatusListeners();
    }
}