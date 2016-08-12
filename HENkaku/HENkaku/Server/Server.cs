using System.Collections.Generic;
using HENkaku.Server.Handler;

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

            var files = new Files ();
            var stage1 = new Stage1 (root + stage2_path);
            var stage2 = new Stage2 (root + pkg_path);

            var handlers = new Dictionary<string, IHandler> () {
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
