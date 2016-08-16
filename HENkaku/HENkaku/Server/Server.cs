using System;
using System.Net;

namespace HENkaku.Server
{
    class Server
    {
        private HttpListener listener = null;
        private readonly string prefix;
        private readonly IRoute route;
        private readonly Log.ILog log;

        public Server (string root, string port, Log.ILog initialLog)
        {
            prefix = "http://*:" + port + "/";
            route = new Route (root);
            log = initialLog;
        }

        private void WaitRequestAsync ()
        {
            log.WriteLine ("Waiting for a new request");
            listener.BeginGetContext (new AsyncCallback (Response), this);
        }

        private static void Response (IAsyncResult result)
        {
            var server = (Server) result.AsyncState;

            try {
                var context = server.listener.EndGetContext (result);
                server.log.WriteLine ("Serving " + context.Request.RawUrl);
                var handler = server.route.GetHandler (context.Request.Url.AbsolutePath);
                handler.Serve (context);
            } catch (Exception exception) {
                server.log.WriteExceptionWarning (exception);
            }

            try {
                server.WaitRequestAsync ();
            } catch (Exception exception) {
                server.log.WriteExceptionError (exception);
                server.Stop ();
            }
        }

        public void Start ()
        {
            log.WriteLine ("Starting");
            listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start ();
            WaitRequestAsync ();
        }

        public void Stop ()
        {
            listener.Close ();
            listener = null;
            log.WriteLine ("Stopped");
        }

        public bool IsRunning ()
        {
            return listener != null;
        }
    }
}
