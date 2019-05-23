using System.Collections.Generic;

namespace MySharp.Logging.Logback.Core.Spi
{
    public interface IPropertyContainer
    {
        string this[string key] {
            get;
        }

        Dictionary<string, string> GetCopyOfProperties();
    }
}