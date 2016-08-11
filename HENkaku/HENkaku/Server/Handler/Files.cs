using System;
using System.Collections.Generic;
using System.IO;

namespace HENkaku.Server.Handler
{
    class Files : Handler
    {
        private readonly Dictionary<string, string> Types;

        public Files ()
        {
            Types = new Dictionary<string, string> () {
                { ".bin", "application/octet-stream" },
                { ".html", "text/html" },
                { ".js", "application/javascript" },
                { ".png", "image/png" },
                { ".sfo", "application/octet-stream" },
                { ".xml", "application/xml" }
            };
        }

        public override void Serve (System.Net.HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            Stream stream = null;
            const string default_resource = "exploit.html";
            string resource = default_resource;
            if (request.Url.AbsolutePath[0] == '/') {
                resource = request.Url.AbsolutePath.Substring (1).Replace ('/', '.');
                try {
                    stream = Resource.Resource.getStream (resource);
                } catch (Exception e) {
                    if (!(e is FileNotFoundException || e is BadImageFormatException))
                        throw;
                }
            }
            if (stream == null) {
                resource = default_resource;
                stream = Resource.Resource.getStream (resource);
            }
            
            var extension = Path.GetExtension (resource);
            response.ContentType = Types[extension];

            response.ContentLength64 = stream.Length;
            stream.CopyTo (response.OutputStream);
            response.Close ();
        }
    }
}
