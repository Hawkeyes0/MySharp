namespace MySharp.Logging.Logback.Core.Status
{
    public interface IStatusListener
    {
        void AddStatusEvent(IStatus status);
    }
}