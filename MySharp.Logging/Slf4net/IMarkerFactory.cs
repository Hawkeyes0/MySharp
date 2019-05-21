namespace MySharp.Logging.Slf4net
{
    public interface IMarkerFactory
    {
        Marker GetMarker(string name);

        bool Exists(string name);

        bool DetachMarker(string name);

        Marker GetDetachedMarker(string name);
    }
}