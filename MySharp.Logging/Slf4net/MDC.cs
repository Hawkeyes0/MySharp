using System;
using System.Collections.Generic;
using System.Text;
using MySharp.Logging.Slf4net.Helpers;
using MySharp.Logging.Slf4net.Impl;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net
{
    public static class MDC
    {
        private static MDCAdapter _mdcAdapter;

        static MDC()
        {
            try
            {
                _mdcAdapter = GetMDCAdapterFromBinder();
            }
            catch (MissingMethodException notype)
            {
                _mdcAdapter = new NopMDCAdapter();
                string msg = notype.Message;
                if (string.IsNullOrEmpty(msg) && msg.Contains("StaticMDCBinder"))
                {
                    Util.Report("Failed to load StaticMDCBinder.", notype);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                Util.Report("MDC Binding unsuccessful.", e);
            }
        }

        private static MDCAdapter GetMDCAdapterFromBinder()
        {
            return StaticMDCBinder.Singleton.GetMDCA();
        }

        public static void Set(string key, string val)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if(_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            _mdcAdapter[key] = val;
        }

        public static string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            return _mdcAdapter[key];
        }

        public static MDCCloseable SetCloseable(string key, string val)
        {
            Set(key,val);
            return new MDCCloseable(key);
        }

        public static void Clear()
        {
            if (_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            _mdcAdapter.Clear();
        }

        public static void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            _mdcAdapter.Remove(key);
        }

        public static Dictionary<string, string> GetCopyOfContext()
        {
            if (_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            return _mdcAdapter.GetCopyOfContext();
        }

        public static void SetContext(Dictionary<string, string> context)
        {
            if (_mdcAdapter == null)
                throw new InvalidOperationException("MDCAdapter cannot be null.");
            _mdcAdapter.SetContextMap(context);
        }

        public static MDCAdapter GetMdcAdapter()
        {
            return _mdcAdapter;
        }
    }

    public class MDCCloseable : IDisposable
    {
        private readonly string _key;

        internal MDCCloseable(string key)
        {
            _key = key;
        }

        public void Dispose()
        {
            MDC.Remove(_key);
        }
    }
}
