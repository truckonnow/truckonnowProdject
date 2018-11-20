using MDispatch.ViewModels.PageAppMV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.PageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Instruction : ContentPage
	{
        private InfoOrderMV infoOrderMV = null;
        public Instruction (InfoOrderMV infoOrderMV)
		{
            this.infoOrderMV = infoOrderMV;
			InitializeComponent ();
            BindingContext = this.infoOrderMV;
		}
	}
}