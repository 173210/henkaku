using HENkaku.Server.Handler;

namespace HENkaku.Server
{
    interface IRoute
    {
        IHandler GetHandler (string path);
    }
}
