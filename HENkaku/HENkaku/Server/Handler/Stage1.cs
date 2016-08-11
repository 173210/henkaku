using System.Text;

namespace HENkaku.Server.Handler
{
    class Stage1 : Payload
    {
        private byte[] js;

        public Stage1 (string uri) : base ("stage1.bin", uri)
        {
        }

        public override void UpdateUri (string uri)
        {
            base.UpdateUri (uri);

            var builder = new StringBuilder ((payload.Length + relocs.Length) * 3);
            builder.Append ("payload=[");

            int index;

            index = 0;
            while (true) {
                builder.Append (System.BitConverter.ToInt32 (payload, index));
                index += 4;
                if (index >= payload.Length)
                    break;

                builder.Append (',');
            }

            builder.Append("];relocs=[");

            index = 0;
            while (true) {
                builder.Append (relocs[index]);
                index++;
                if (index >= relocs.Length)
                    break;

                builder.Append (',');
            }

            builder.Append("];");

            js = Encoding.ASCII.GetBytes (builder.ToString ());
        }

        public override void Serve (System.Net.HttpListenerContext context)
        {
            context.Response.ContentType = "application/javascript";
            context.Response.ContentLength64 = js.Length;
            context.Response.OutputStream.Write(js, 0, js.Length);
            context.Response.Close ();
        }
    }
}
