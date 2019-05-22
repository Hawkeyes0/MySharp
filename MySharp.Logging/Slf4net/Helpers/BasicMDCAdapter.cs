using System.Collections.Generic;
using MySharp.Logging.Slf4net.Spi;

namespace MySharp.Logging.Slf4net.Helpers
{
    public class BasicMDCAdapter : MDCAdapter
    {
        private Dictionary<string,string> _dict = new Dictionary<string, string>();

        public string this[string key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public void Remove(string key)
        {
            _dict.Remove(key);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        public Dictionary<string, string> GetCopyOfContext()
        {
            return new Dictionary<string, string>(_dict);
        }

        public void SetContextMap(Dictionary<string, string> context)
        {
            _dict = new Dictionary<string, string>(context);
        }
    }
}