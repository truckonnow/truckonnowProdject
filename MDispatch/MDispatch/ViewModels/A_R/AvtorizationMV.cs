using MDispatch.Service;
using MDispatch.Service.GeloctionGPS;
using MDispatch.View;
using MDispatch.View.TabPage;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels
{
    class AvtorizationMV : BindableBase
    {
        ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand AvtorizationCommand { get; set; }

        public AvtorizationMV()
        {
            managerDispatchMob = new ManagerDispatchMob();
            AvtorizationCommand = new DelegateCommand(Avtorization);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }

        private string feedBack;
        public string FeedBack
        {
            get { return feedBack; }
            set
            {
                SetProperty(ref feedBack, value);
            }
        }
         
        private async void Avtorization()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = null;
            string description = null;
            int state = 3;
            await Task.Run(() =>
            {
                state = managerDispatchMob.A_RWork("authorisation", Username, Password, ref description, ref token);
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                FeedBack = "Not Network";
            }
            else if(state == 2)
            {
                FeedBack = description;
            }
            else if(state == 3)
            {
                CrossSettings.Current.AddOrUpdateValue("Token", token);
                Application.Current.MainPage = new NavigationPage(new TabPage(managerDispatchMob));
                await Utils.StartListening();
            }
            else if(state == 4)
            {
                FeedBack = "Technical work on the service";
            }
        }
    }
}