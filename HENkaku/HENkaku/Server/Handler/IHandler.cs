namespace HENkaku.Server.Handler
{
    abstract class IHandler
    {
        public abstract void Serve (System.Net.HttpListenerContext response);
    }
}
