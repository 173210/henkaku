using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HENkaku.Server.Handler;

namespace HENkaku.Server
{
    class Listener
    {
        private readonly HttpListener listener;
        private readonly IHandler defaultHandler;
        private readonly IReadOnlyDictionary<string, IHandler> handlers;
        private readonly Task task;

        public Listener (IHandler initialDefaultHandler,
            IReadOnlyDictionary<string, IHandler> initialHandlers,
            string port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + port + "/");
            defaultHandler = initialDefaultHandler;
            handlers = initialHandlers;
            task = new Task (Listen);
        }

        private void Listen ()
        {
            listener.Start ();
            
            while (true) {
                var context = listener.GetContext ();
                
                IHandler handler;
                if (!handlers.TryGetValue (context.Request.Url.AbsolutePath, out handler))
                    handler = defaultHandler;
                
                handler.Serve (context);
            }
        }

        public void Start ()
        {
            task.Start ();
        }
    }
}
