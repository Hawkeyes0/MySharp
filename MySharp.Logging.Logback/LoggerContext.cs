using MySharp.Logging.Logback.Core;
using MySharp.Logging.Logback.Core.Spi;
using MySharp.Logging.Slf4net;

namespace MySharp.Logging.Logback.Classic
{
    public class LoggerContext : ContextBase, ILoggerFactory, ILifeCycle
    {
        public string Name { get; set; }
        public Logger GetLogger(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}