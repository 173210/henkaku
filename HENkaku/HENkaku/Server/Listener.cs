using System;
using System.Net;

namespace HENkaku.Server
{
    class Listener
    {
        private readonly HttpListener listener;
        private readonly IRoute route;

        public Listener (IRoute initialRoute, string port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + port + "/");
            route = initialRoute;
        }

        private void WaitRequestAsync ()
        {
            listener.BeginGetContext (new AsyncCallback (Response), this);
        }

        private static void Response (IAsyncResult result)
        {
            var listener = (Listener) result.AsyncState;
            var context = listener.listener.EndGetContext (result);
            var handler = listener.route.GetHandler (context.Request.Url.AbsolutePath);
            handler.Serve (context);
            listener.WaitRequestAsync ();
        }

        public void Start ()
        {
            listener.Start ();
            WaitRequestAsync ();
        }
    }
}
