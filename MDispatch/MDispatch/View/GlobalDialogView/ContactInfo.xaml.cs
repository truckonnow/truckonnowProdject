using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.GlobalDialogView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactInfo : PopupPage
    {
        public ContactInfo()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("http://truckonnow.com", BrowserLaunchMode.SystemPreferred);
        }

        [Obsolete]
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new SendMail());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            PhoneDialer.Open("+17734305155");
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("+17734305155");
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("truckonnow_LTD@gmail.com");
        }

        private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("Artyom");
        }

        private async void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("http://truckonnow.com/");
        }

        private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("Truckonnow ltd");
        }

        private async void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}