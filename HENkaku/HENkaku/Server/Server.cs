using System.Collections.Generic;

namespace HENkaku.Server
{
    class Server
    {
        private readonly Listener listener;
        
        public Server (string root, string port)
        {
            const string stage1_path = "/payload.js";
            const string stage2_path = "/x";
            const string pkg_path = "/pkg";

            var files = new Handler.Files ();
            var stage1 = new Handler.Stage1 (root + stage2_path);
            var stage2 = new Handler.Stage2 (root + pkg_path);

            var handlers = new Dictionary<string, Handler.Handler> () {
                {  stage1_path, stage1 },
                {  stage2_path, stage2 }
            };

            listener = new Listener (files, handlers, port);
        }

        public void Start ()
        {
            listener.Start ();
        }
    }
}
