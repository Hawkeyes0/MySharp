using System.Collections.Generic;

namespace MySharp.Logging.Slf4net.Spi
{
    public interface MDCAdapter
    {
        string this[string key] { get; set; }

        void Remove(string key);

        void Clear();

        Dictionary<string, string> GetCopyOfContext();

        void SetContextMap(Dictionary<string, string> context);
    }
}