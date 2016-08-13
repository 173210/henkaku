namespace HENkaku.Server
{
    class Server
    {
        private readonly Listener listener;
        
        public Server (string root, string port)
        {
            var route = new Route (root);
            listener = new Listener (route, port);
        }

        public void Start (Log.ILog log)
        {
            listener.Start (log);
        }
    }
}
