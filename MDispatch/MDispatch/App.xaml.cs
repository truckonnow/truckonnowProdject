using MDispatch.Service.GeloctionGPS;
using MDispatch.StoreNotify;
using MDispatch.View.A_R;
using MDispatch.View.TabPage;
using Plugin.Settings;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch
{
    public partial class App : Application
	{
        public static bool isAvtorization;
        public static bool isInspection;
        public static bool isNetwork;
        public static bool isStart;
            
        public App ()
		{
			InitializeComponent();
        }

		protected override async void OnStart ()
        {
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
            if (isAvtorization)
            {
                Task.Run(async () =>
                {
                    DependencyService.Get<IStore>().OnTokenRefresh();
                });
                isStart = true;
                await Utils.StartListening();
            }
        }

		protected override async void OnSleep ()
        {
            if (isAvtorization)
            {
                isStart = false;
                   await Utils.StopListening();
            }
        }

		protected override async void OnResume ()
        {
            if (isAvtorization)
            {
                isStart = true;
                await Utils.StartListening();
            }
        }
	}
}