using System.Net;
using Xamarin.Forms;

namespace HENkaku
{
    class MainModel
    {
        private readonly Log.Backend logBackend;
        private readonly Log.Frontend logFrontend;
        private readonly Server.Server server;
        private readonly string uri;

        public MainModel ()
        {
            logBackend = new Log.Backend ();
            logBackend.WriteLine ("Initializing");

            try {
				LogClear = new Command (logBackend.Clear);

                const string port = "8080";

                var hostName = Dns.GetHostName ();
                var hostAddress = Dns.GetHostAddresses (hostName);
                uri = "http://" + hostAddress[0].ToString () + ":" + port;
                logFrontend = new Log.Frontend (logBackend);
            
                server = new Server.Server (uri, port, logFrontend);
                server.Start ();
            } catch (System.Exception exception) {
                logBackend.WriteExceptionError (exception);
            }
        }

        public System.Windows.Input.ICommand LogClear
        {
			get;
			protected set;
        }

        public bool ServerSwitch
        {
            get
            {
                // return server.IsRunning ();
                return true;
            }
            set
            {
                if (value) {
                    if (!server.IsRunning ())
                        server.Start ();
                } else {
                    if (server.IsRunning ())
                        server.Stop ();
                }
            }
        }

        public FormattedString Log
        {
            get
            {
                return logBackend.Get ();
            }
        }

        public string Uri
        {
            get
            {
                return "URI: " + uri;
            }
        }
    }
}
