using System.Collections.Generic;
using HENkaku.Server.Handler;

namespace HENkaku.Server
{
    class Route : IRoute
    {
        private readonly Handler.IHandler DefaultHandler;
        private readonly IReadOnlyDictionary<string, IHandler> Handlers;

        public Route (string root)
        {
            const string stage1_path = "/payload.js";
            const string stage2_path = "/x";
            const string pkg_path = "/pkg";

            var files = new Files ();
            var stage1 = new Stage1 (root + stage2_path);
            var stage2 = new Stage2 (root + pkg_path);
            
            DefaultHandler = files;
            Handlers = new Dictionary<string, IHandler> () {
                {  stage1_path, stage1 },
                {  stage2_path, stage2 }
            };
        }

        public override IHandler GetHandler (string path)
        {
            IHandler handler;

            if (!Handlers.TryGetValue (path, out handler))
                handler = DefaultHandler;

            return handler;
        }
    }
}
