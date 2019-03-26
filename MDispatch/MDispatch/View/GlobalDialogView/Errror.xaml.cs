using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.GlobalDialogView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Errror : PopupPage
    {
        public Errror(string info)
        {
            InitializeComponent();
            infoL.Text = info;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}