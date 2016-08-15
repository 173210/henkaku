using System;
using System.Net;

namespace HENkaku.Server
{
    class Server
    {
        private readonly HttpListener listener;
        private readonly IRoute route;

        public Server (string root, string port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + port + "/");
            route = new Route (root);
        }

        private void WaitRequestAsync (Log.ILog log)
        {
            log.WriteLine ("Waiting for a new request");
            listener.BeginGetContext (new AsyncCallback (Response), log);
        }

        private void Response (IAsyncResult result)
        {
            var log = (Log.ILog) result.AsyncState;

            try {
                var context = listener.EndGetContext (result);
                log.WriteLine ("Serving " + context.Request.RawUrl);
                var handler = route.GetHandler (context.Request.Url.AbsolutePath);
                handler.Serve (context);
            } catch (Exception exception) {
                log.WriteExceptionWarning (exception);
            }

            try {
                WaitRequestAsync (log);
            } catch (Exception exception) {
                log.WriteExceptionError (exception);
                log.WriteLine ("Stopped. Please restart application.");
            }
        }

        public void Start (Log.ILog log)
        {
            listener.Start ();
            WaitRequestAsync (log);
        }
    }
}
