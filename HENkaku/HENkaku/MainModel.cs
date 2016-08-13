using System.Net;
using Xamarin.Forms;

namespace HENkaku
{
    class MainModel
    {
        private readonly Log.Backend log;
        private readonly string uri;

        public MainModel ()
        {
            log = new Log.Backend ();
            log.WriteLine ("Starting");

            try {
				LogClear = new Command (log.Clear);

                const string port = "8080";

                var hostName = Dns.GetHostName ();
                var hostAddress = Dns.GetHostAddresses (hostName);
                uri = "http://" + hostAddress[0].ToString () + ":" + port;
            
                var server = new Server.Server (uri, port);
                var frontend = new Log.Frontend (log);
                server.Start (frontend);
            } catch (System.Exception exception) {
                log.WriteExceptionError (exception);
            }
        }

        public System.Windows.Input.ICommand LogClear
        {
			get;
			protected set;
        }

        public FormattedString Log
        {
            get
            {
                return log.Get ();
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
