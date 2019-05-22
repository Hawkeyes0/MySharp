namespace MySharp.Logging.Slf4net.Spi
{
    public interface LoggerFactoryBinder
    {
        ILoggerFactory GetLoggerFactory();

        string GetLoggerFactoryClassStr();
    }
}