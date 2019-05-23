using System;
using MySharp.Logging.Logback.Core.Spi;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core
{
    public interface IContext : IPropertyContainer
    {
        IStatusManager StatusManager { get; }

        object GetObject(string key);

        void PutObject(string key, object value);

        string GetProperty(string key);

        void PutProperty(string key, string value);

        string Name { get; set; }

        DateTime BirthTime { get; }

        object ConfigurationLock { get; }

        void Register(ILifeCycle component);


    }
}