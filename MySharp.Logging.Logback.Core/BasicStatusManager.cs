using System;
using System.Collections.Generic;
using MySharp.Logging.Logback.Core.Spi;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core
{
    public class BasicStatusManager : IStatusManager
    {
        public const int MaxHeaderCount = 150;
        public const int TailSize = 150;

        protected readonly List<IStatus> StatusList = new List<IStatus>();
        protected readonly List<IStatus> TailBuffer = new List<IStatus>();
        protected readonly LogbackLock StatusListLock = new LogbackLock();

        protected readonly List<IStatusListener> StatusListenerlList = new List<IStatusListener>();
        protected LogbackLock StatusListenerListLock = new LogbackLock();

        private int _level = Status.Status.Info;

        public void Add(IStatus status)
        {
            FireStatusAddEvent(status);

            Count++;
            if (status.Level > _level)
                _level = status.Level;

            lock (StatusListLock)
            {
                if (StatusList.Count < MaxHeaderCount)
                    StatusList.Add(status);
                else
                    TailBuffer.Add(status);
            }
        }

        private void FireStatusAddEvent(IStatus status)
        {
            lock (StatusListenerListLock)
            {
                foreach (IStatusListener listener in StatusListenerlList)
                {
                    listener.AddStatusEvent(status);
                }
            }
        }

        public List<IStatus> GetCopyOfStatuses()
        {
            lock (StatusListLock)
            {
                List<IStatus> list = new List<IStatus>(StatusList);
                list.AddRange(TailBuffer);
                return list;
            }
        }

        public int Count { get; protected set; }

        public int Level => _level;

        public bool Add(IStatusListener listener)
        {
            lock (StatusListenerListLock)
            {
                if (listener is OnConsoleStatusListener)
                {
                    bool alreadyPresent = CheckForPresent(StatusListenerlList, listener.GetType());
                    if (alreadyPresent) return false;
                }
                StatusListenerlList.Add(listener);
            }

            return true;
        }

        private bool CheckForPresent(List<IStatusListener> statusListenerlList, Type type)
        {
            foreach (IStatusListener listener in statusListenerlList)
            {
                if (listener.GetType() == type)
                    return true;
            }

            return false;
        }

        public void Remove(IStatusListener listener)
        {
            lock (StatusListenerListLock)
            {
                StatusListenerlList.Remove(listener);
            }
        }

        public void Clear()
        {
            lock (StatusListLock)
            {
                Count = 0;
                StatusList.Clear();
                TailBuffer.Clear();
            }
        }

        public List<IStatusListener> GetCopyOfStatusListeners()
        {
            lock (StatusListenerListLock)
            {
                return new List<IStatusListener>(StatusListenerlList);
            }
        }
    }
}