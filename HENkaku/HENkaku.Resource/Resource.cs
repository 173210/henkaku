using System.Reflection;

namespace HENkaku.Resource
{
    public class Resource
    {
        public static System.IO.Stream getStream(string name)
        {
            var path = "HENkaku.Resource." + name;
            var assembly = typeof (Resource).GetTypeInfo ().Assembly;
            return assembly.GetManifestResourceStream (path);
        }
    }
}
