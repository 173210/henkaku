namespace HENkaku.Server.Handler
{
    abstract class Payload : IHandler
    {
        private readonly int uriIndex;
        private const int uriMax = 256;
        protected byte[] payload;
        protected readonly byte[] relocs;

        public Payload (string name, string uri)
        {
            var stream = Resource.Resource.getStream(name);
            var reader = new System.IO.BinaryReader (stream);

            var payloadWords = reader.ReadUInt32 ();

            payload = new byte[payloadWords * 4];
            reader.Read (payload, 0, payload.Length);

            relocs = new byte[payloadWords];
            reader.Read (relocs, 0, relocs.Length);

            var count = 0;
            uriIndex = 0;
            while (count < uriMax) {
                if (payload[uriIndex + count] == 'x') {
                    count++;
                } else {
                    uriIndex += count + 1;
                    count = 0;
                }
            }

            UpdateUri (uri);
        }

        public virtual void UpdateUri (string uri)
        {
            if (uri.Length >= uriMax)
                throw new System.Exception ("The maximum URI length is 255 characters.");

            var uriBytes = System.Text.Encoding.ASCII.GetBytes (uri);
            int cursor;
            for (cursor = 0; cursor < uriBytes.Length; cursor++)
                payload[uriIndex + cursor] = uriBytes[cursor];

            payload[uriIndex + cursor] = 0;
        }

        public abstract void Serve (System.Net.HttpListenerContext context);
    }
}
