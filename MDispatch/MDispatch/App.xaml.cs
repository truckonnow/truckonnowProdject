using MDispatch.Service.GeloctionGPS;
using MDispatch.View.A_R;
using MDispatch.View.TabPage;
using Plugin.Settings;
using Xamarin.Forms;

namespace MDispatch
{
    public partial class App : Application
	{
        bool isAvtorization;
            
        public App ()
		{
			InitializeComponent();
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            if (token == "")
            {
                MainPage = new NavigationPage(new Avtorization());
                isAvtorization = false;
            }
            else
            {
                MainPage = new NavigationPage(new TabPage(new Service.ManagerDispatchMob()));
                isAvtorization = true;
            }
        }

		protected override async void OnStart ()
        {
            if (isAvtorization)
            {
                await Utils.StartListening();
            }
        }

		protected override async void OnSleep ()
        {
            if (isAvtorization)
            {
                await Utils.StopListening();
            }
        }

		protected override async void OnResume ()
        {
            if (isAvtorization)
            {
                await Utils.StartListening();
            }
        }
	}
}