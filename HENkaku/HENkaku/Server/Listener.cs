using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HENkaku.Server
{
    class Listener
    {
        private readonly HttpListener listener;
        private readonly Handler.Handler defaultHandler;
        private readonly IReadOnlyDictionary<string, Handler.Handler> handlers;
        private readonly Task task;

        public Listener (Handler.Handler initialDefaultHandler,
            IReadOnlyDictionary<string, Handler.Handler> initialHandlers,
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
                
                Handler.Handler handler;
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
