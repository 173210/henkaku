using System.Net;
using Xamarin.Forms;

namespace HENkaku
{
	public class App : Application
	{
		public App ()
		{
            const string port = "8080";

            var hostName = Dns.GetHostName ();
            var hostAddress = Dns.GetHostAddresses (hostName);
            var uri = "http://" + hostAddress[0].ToString () + ":" + port;

            var uriLabel = new Label {
                Text = "URI: " + uri
            };

            MainPage = new ContentPage {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        uriLabel
					}
				}
			};

            var server = new Server.Server (uri, port);
            server.Start ();
        }
	}
}
