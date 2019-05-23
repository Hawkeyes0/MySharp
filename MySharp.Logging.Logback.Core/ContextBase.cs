using System;
using System.Collections.Generic;
using MySharp.Logging.Logback.Core.Spi;
using MySharp.Logging.Logback.Core.Status;

namespace MySharp.Logging.Logback.Core
{
    public class ContextBase : IContext, ILifeCycle
    {
        private bool started;
        private LogbackLock _configurationLock = new LogbackLock();
        private Dictionary<string, string> _properties = new Dictionary<string, string>();
        private Dictionary<string, object> _objects = new Dictionary<string, object>();

        public ContextBase()
        {
            InitCollision();
        }

        private void InitCollision()
        {
            PutObject(CoreConstants.FaFilenameCollisions, new Dictionary<string,string>());
            PutObject(CoreConstants.RfaFilenamePatternCollisions, new Dictionary<string, FileNamePattern>());
        }

        public string this[string key]
        {
            get
            {
                if (CoreConstants.ContextNameKey == key)
                    return Name;
                if (CoreConstants.HostnameKey == key)
                    return LazyGetHostname();
                return _properties[key];
            }
            set
            {
                if (CoreConstants.HostnameKey == key)
                    PutHostnameProperty(value);
                else
                    _properties[key] = value;
            }
        }

        private void PutHostnameProperty(string hostname)
        {
            string exists = _properties[CoreConstants.HostnameKey];
            if (string.IsNullOrEmpty(exists))
                _properties[CoreConstants.HostnameKey] = hostname;
        }

        private string LazyGetHostname()
        {
            string hostname = _properties[CoreConstants.HostnameKey];
            if (string.IsNullOrEmpty(hostname))
            {
                hostname = NetworkUtil.GetLocalHostname(this);
                PutHostnameProperty(hostname);
            }

            return hostname;
        }

        public Dictionary<string, string> GetCopyOfProperties()
        {
            return new Dictionary<string, string>(_properties);
        }

        private IStatusManager _sm = new BasicStatusManager();
        public IStatusManager StatusManager
        {
            get => _sm;
            set => _sm = value ?? throw new ArgumentNullException();
        }

        public object GetObject(string key) => _objects[key];

        public void PutObject(string key, object value) => _objects[key] = value;

        public string GetProperty(string key) => this[key];

        public void PutProperty(string key, string value) => _properties[key] = value;

        public string Name { get; set; }
        public DateTime BirthTime { get; } = DateTime.Now;
        public object ConfigurationLock => _configurationLock;
        public void Register(ILifeCycle component)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            started = true;
        }

        public void Stop()
        {
            started = false;
        }

        public bool IsStarted => started;
    }
}