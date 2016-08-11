namespace HENkaku.Server.Handler
{
    abstract class Handler
    {
        public abstract void Serve (System.Net.HttpListenerContext response);
    }
}
