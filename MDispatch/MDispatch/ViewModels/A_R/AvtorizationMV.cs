using FormsControls.Base;
using MDispatch.Service;
using MDispatch.Service.GeloctionGPS;
using MDispatch.Service.Tasks;
using MDispatch.StoreNotify;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.TabPage;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels
{
    public class AvtorizationMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;
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

        private string fullName;
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
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

        private string passwordForgot;
        public string PasswordForgot
        {
            get { return passwordForgot; }
            set
            {
                SetProperty(ref passwordForgot, value);
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

        private string feedBack1 = "";
        public string FeedBack1
        {
            get { return feedBack1; }
            set
            {
                SetProperty(ref feedBack1, value);
            }
        }

        [System.Obsolete]
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
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack = "Not Network";
            }
            else if(state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack = description;
            }
            else if(state == 3)
            {
                App.isAvtorization = true;
                CrossSettings.Current.AddOrUpdateValue("Token", token.Split(',')[0]);
                CrossSettings.Current.AddOrUpdateValue("IdDriver", token.Split(',')[1]);
                CrossSettings.Current.AddOrUpdateValue("Username", Username);
                CrossSettings.Current.AddOrUpdateValue("Password", Password);
                await Task.Run(() =>
                {
                    DependencyService.Get<IStore>().OnTokenRefresh();
                    Utils.StartListening();
                    TaskManager.CommandToDo("CheckTask");
                });
                Application.Current.MainPage = new AnimationNavigationPage(new TabPage(managerDispatchMob));
            }
            else if(state == 4)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack = "Technical work on the service";
            }
        }

        
        [Obsolete]
        public async void RequestPasswordChanges()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = null;
            string description = null;
            int state = 3;
            await Task.Run(() =>
            {
                state = managerDispatchMob.A_RWork("RequestPasswordChanges", FullName, PasswordForgot, ref description, ref token);
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack1 = "Not Network";
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack1 = description;
            }
            else if (state == 3)
            {
                await PopupNavigation.PopAllAsync();
                FeedBack1 = "";
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
                FeedBack1 = "Technical work on the service";
            }
        }
    }
}