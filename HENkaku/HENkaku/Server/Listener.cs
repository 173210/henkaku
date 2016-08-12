using System.Net;
using System.Threading.Tasks;

namespace HENkaku.Server
{
    class Listener
    {
        private readonly HttpListener listener;
        private readonly IRoute route;
        private readonly Task task;

        public Listener (IRoute initialRoute, string port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + port + "/");
            route = initialRoute;
            task = new Task (Listen);
        }

        private void Listen ()
        {
            listener.Start ();
            
            while (true) {
                var context = listener.GetContext ();
                var handler = route.GetHandler (context.Request.Url.AbsolutePath);
                
                handler.Serve (context);
            }
        }

        public void Start ()
        {
            task.Start ();
        }
    }
}
