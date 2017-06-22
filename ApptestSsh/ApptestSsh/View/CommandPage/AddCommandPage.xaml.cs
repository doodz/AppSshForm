using Doods.StdFramework.Mvvm;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.CommandPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCommandPage : ViewPage<AddCommandPageViewModel>
	{
	
		public AddCommandPage ()
		{
			InitializeComponent ();
		}
	}
}