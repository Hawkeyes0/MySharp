using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySharp.Logging.Logback.Core.Spi;
using MySharp.Logging.Logback.Core.Util;

namespace MySharp.Logging.Logback.Core.Status
{
    public abstract class OnPrintStreamStatusListenerBase : ContextAwareBase, IStatusListener, ILifeCycle
    {
        private bool _started;

        private static readonly TimeSpan DefaultRetrospective = TimeSpan.FromTicks(300);

        public TimeSpan RetrospectiveThresold { get; set; } = DefaultRetrospective;

        public string Prefix { get; set; }

        protected abstract TextWriter GetWriter();

        private void Print(IStatus status)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(Prefix))
                sb.Append(Prefix);

            StatusPrinter.BuildStr(sb, "", status);
            GetWriter().Write(sb);
        }

        public void AddStatusEvent(IStatus status)
        {
            if (!_started)
                return;
            Print(status);
        }

        private void RetrospectivePrint()
        {
            if (_context == null)
                return;
            DateTime now = DateTime.Now;
            IStatusManager sm = _context.StatusManager;
            List<IStatus> statusList = sm.GetCopyOfStatuses();
            foreach (IStatus status in statusList)
            {
                DateTime time = status.Date;
                if(IsElapsedTimeLongerThanThreshold(now, time)) 
                    Print(status);
            }
        }

        private bool IsElapsedTimeLongerThanThreshold(DateTime now, DateTime time)
        {
            return now - time < RetrospectiveThresold;
        }

        public void Start()
        {
            _started = true;
            if (RetrospectiveThresold.Ticks > 0)
            {
                RetrospectivePrint();
            }
        }

        public void Stop()
        {
            _started = false;
        }

        public bool IsStarted => _started;
    }
}