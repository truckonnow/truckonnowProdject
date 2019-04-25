using MDispatch.Service.GeloctionGPS;
using MDispatch.View.A_R;
using MDispatch.View.TabPage;
using Plugin.Settings;
using Xamarin.Forms;

namespace MDispatch
{
    public partial class App : Application
	{
        public static bool isAvtorization;
        public static bool isInspection;
            
        public App ()
		{
			InitializeComponent();
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            if (token == "")
            {
                isAvtorization = false;
                MainPage = new NavigationPage(new Avtorization());
            }
            else
            {
                isAvtorization = true;
                MainPage = new NavigationPage(new TabPage(new Service.ManagerDispatchMob()));
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