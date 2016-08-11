namespace HENkaku.Server.Handler.Resource
{
    class Resource
    {
        public static System.IO.Stream getStream(string name)
        {
#if __ANDROID__
            const string target = "Droid";
#elif __IOS__
            const string target = "iOS";
#endif
            var path = "HENkaku." + target + ".Server.Handler.Resource." + name;
            var assembly = typeof (Resource).Assembly;
            return assembly.GetManifestResourceStream (path);
        }
    }
}
