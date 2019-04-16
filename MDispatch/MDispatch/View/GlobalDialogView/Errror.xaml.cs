using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.GlobalDialogView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Errror : PopupPage
    {
        private INavigation navigation = null;

        public Errror(string info, INavigation navigation)
        {
            this.navigation = navigation;
            InitializeComponent();
            infoL.Text = info;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
            if(navigation != null)
            {
                await navigation.PopAsync();
            }
        }
    }
}