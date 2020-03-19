using MDispatch.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : PopupPage
    {
        private AvtorizationMV avtorizationMV = null;

        public ForgotPassword(AvtorizationMV avtorizationMV)
        {
            this.avtorizationMV = avtorizationMV;
            InitializeComponent();
            BindingContext = this.avtorizationMV;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (IsValidEmail(e.NewTextValue))
            {
                notCorectEmailL.IsVisible = false;
                entry.TextColor = Color.Default;
            }
            else
            {
                notCorectEmailL.IsVisible = true;
                entry.TextColor = Color.Red;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (IsValidEmail(emailE.Text))
            {
                notCorectEmailL.IsVisible = false;
                emailE.TextColor = Color.Default;
                avtorizationMV.RequestPasswordChanges();
            }
            else
            {
                notCorectEmailL.IsVisible = true;
                emailE.TextColor = Color.Red;
            }
        }
    }
}