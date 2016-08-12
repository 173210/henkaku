using System;
using System.IO;

namespace HENkaku.Server.Handler
{
    class Stage2 : Payload
    {
        public Stage2 (string uri) : base ("stage2.bin", uri)
        {
        }

        private static UInt32[] ParseQuery (string query)
        {
            if (query[0] != '?')
                throw new Exception ("The query for stage 2 doesn't include valid query");
            
            var parameters = query.Substring (1).Split ('&');
            var args = new UInt32[8];
            foreach (var arg in parameters) {
                var pair = arg.Split ('=');
                if (pair.Length != 2)
                    continue;

                if (pair[0][0] != 'a')
                    throw new Exception ("Unknown parameter for stage2");

                var index = int.Parse (pair[0].Substring (1));
                var value = Convert.ToUInt32(pair[1], 16);
                args[index] = value;
            }

            return args;
        }

        private void Relocate (BinaryWriter writer, UInt32[] args, int start, int end)
        {
            for (var index = start; index < end; index += 4) {
                var old = BitConverter.ToUInt32 (payload, index);
                writer.Write (old + args[relocs[index / 4]]);
            }
        }

        public override void Serve (System.Net.HttpListenerContext context)
        {
            var args = ParseQuery (context.Request.Url.Query);
            var dsize = BitConverter.ToUInt32 (payload, 0x10);
            var csize = BitConverter.ToUInt32 (payload, 0x20);
            const uint dstart = 0x40;
            var cstart = dstart + dsize;
            
            args[0] = 0;
            args[1] += csize;
            
            var memory = new MemoryStream ((int)(dsize + csize));

            var writer = new BinaryWriter (memory);
            Relocate (writer, args, (int)cstart, (int)(cstart + csize));
            Relocate (writer, args, (int)dstart, (int)cstart);
            memory.Seek (0, SeekOrigin.Begin);
            
            context.Response.ContentLength64 = memory.Length;
            context.Response.ContentType = "application/octet-stream";
            memory.CopyTo (context.Response.OutputStream);
            context.Response.Close ();
        }
    }
}
