namespace HENkaku.Server.Handler
{
    interface IHandler
    {
        void Serve (System.Net.HttpListenerContext response);
    }
}
