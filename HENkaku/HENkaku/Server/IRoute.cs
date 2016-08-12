using HENkaku.Server.Handler;

namespace HENkaku.Server
{
    abstract class IRoute
    {
        public abstract IHandler GetHandler (string path);
    }
}
