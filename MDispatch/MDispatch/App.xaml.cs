using MDispatch.View.A_R;
using MDispatch.View.TabPage;
using Plugin.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MDispatch
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            if (token == "")
            {
                MainPage = new NavigationPage(new Avtorization());
            }
            else
            {
                MainPage = new TabPage(new Service.ManagerDispatchMob());
            }
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}