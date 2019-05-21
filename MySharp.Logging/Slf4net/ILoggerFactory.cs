namespace MySharp.Logging.Slf4net
{
    public interface ILoggerFactory
    {
        Logger GetLogger(string name);
    }
}