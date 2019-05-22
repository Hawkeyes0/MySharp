using System.Collections.Generic;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class NopMDCAdapter : MDCAdapter
    {
        public string this[string key]
        {
            get => null;
            set { }
        }

        public void Remove(string key)
        {
        }

        public void Clear()
        {
        }

        public Dictionary<string, string> GetCopyOfContext() => null;

        public void SetContextMap(Dictionary<string, string> context)
        {
        }
    }
}