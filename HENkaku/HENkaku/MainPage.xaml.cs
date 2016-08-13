using Xamarin.Forms;

namespace HENkaku
{
	partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			this.BindingContext = new MainModel ();
			InitializeComponent ();
		}
	}
}
