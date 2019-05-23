namespace MySharp.Logging.Logback.Core.Spi
{
    public interface ILifeCycle
    {
        void Start();

        void Stop();

        bool IsStarted { get; }
    }
}