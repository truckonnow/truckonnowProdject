using MDispatch.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.A_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Avtorization : ContentPage
	{
        private  AvtorizationMV avtorizationMV = null;

        public Avtorization ()
		{
            InitializeComponent ();
            avtorizationMV = new AvtorizationMV();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = avtorizationMV;
            Init();
        }

        private async void Init()
        {
            await Task.WhenAll(
                    lUsName.TranslateTo(0, 33, 0),
                    lUsName.FadeTo(0.5, 0),
                    lPassword.TranslateTo(0, 33, 0),
                    lPassword.FadeTo(0.5, 0)
                    );
        }

        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length != 0)
            {
                await Task.WhenAll(
                    lPassword.TranslateTo(0, 13, 150),
                    lPassword.FadeTo(1, 150)
                    );
            }
            else
            {
                await Task.WhenAll(
                    lPassword.TranslateTo(0, 33, 150),
                    lPassword.FadeTo(0.5, 150)
                    );
            }
        }

        private async void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ValidationInpEmail(e.NewTextValue);
            if (e.NewTextValue.Length != 0)
            {
                await Task.WhenAll(
                    lUsName.TranslateTo(0, 13, 150),
                    lUsName.FadeTo(1, 150)
                    );
            }
            else
            {
                await Task.WhenAll(
                    lUsName.TranslateTo(0, 33, 150),
                    lUsName.FadeTo(0.5, 150)
                    );
            }
        }

        private void ValidationInpEmail(string email)
        {
            if(IsValidEmail(email))
            {
                eUsName.TextColor = Color.Default;
            }
            else
            {
                eUsName.TextColor = Color.Red;
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
    }
}