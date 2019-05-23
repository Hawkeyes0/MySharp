using System;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core.Spi
{
    public class ContextAwareBase : IContextAware
    {
        protected IContext _context;
        private int _noContextWarning;

        public ContextAwareBase()
        {
            DeclaredOrigin = this;
        }

        public ContextAwareBase(IContextAware declareOrigin)
        {
            DeclaredOrigin = declareOrigin;
        }

        public IContext Context
        {
            get => _context;
            set
            {
                if (_context == null) _context = value;
                else if (_context != value) throw new InvalidOperationException("Context has been already set");
            }
        }

        public IStatusManager StatusManager => _context?.StatusManager;

        protected object DeclaredOrigin { get; }

        public void AddStatus(IStatus status)
        {
            if (_context == null)
            {
                if (_noContextWarning++ == 0)
                {
                    Console.WriteLine($"Logback: No context given for {this}.");
                }

                return;
            }

            IStatusManager sm = _context.StatusManager;
            sm?.Add(status);
        }

        public void AddInfo(string msg, Exception ex = null)
        {
            AddStatus(new InfoStatus(msg, DeclaredOrigin, ex));
        }

        public void AddWarn(string msg, Exception ex = null)
        {
            AddStatus(new WarnStatus(msg, DeclaredOrigin, ex));
        }

        public void AddError(string msg, Exception ex = null)
        {
            AddStatus(new ErrorStatus(msg, DeclaredOrigin, ex));
        }
    }
}