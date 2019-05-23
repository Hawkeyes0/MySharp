using System;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core.Spi
{
    public interface IContextAware
    {
        IContext Context { get; set; }

        void AddStatus(IStatus status);

        void AddInfo(string msg, Exception ex = null);

        void AddWarn(string msg, Exception ex = null);

        void AddError(string msg, Exception ex = null);
    }
}
