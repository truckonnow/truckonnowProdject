using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV.Settings
{
    public class SettingsMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;

        [System.Obsolete]
        public SettingsMV(ManagerDispatchMob managerDispatchMob)
        {
            this.managerDispatchMob = managerDispatchMob;
            Init();
        }
        
        public INavigation Navigation { get; set; }

        private string latsInspection = "--.--.--";
        public string LatsInspection
        {
            get { return latsInspection; }
            set { SetProperty(ref latsInspection, value); }
        }

        private string plateTruck = "---------";
        public string PlateTruck
        {
            get { return plateTruck; }
            set { SetProperty(ref plateTruck, value); }
        }

        private string plateTrailer = "---------";
        public string PlateTrailer
        {
            get { return plateTrailer; }
            set { SetProperty(ref plateTrailer, value); }
        }

        private string plateTruck1 = "";
        public string PlateTruck1
        {
            get { return plateTruck1; }
            set { SetProperty(ref plateTruck1, value); }
        }

        private string plateTrailer1 = "";
        public string PlateTrailer1
        {
            get { return plateTrailer1; }
            set { SetProperty(ref plateTrailer1, value); }
        }

        [System.Obsolete]
        private async void Init()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string idDriver = CrossSettings.Current.GetValueOrDefault("IdDriver", "");
            string description = null;
            int state = 0; 
            string latsInspection = "--.--.--";
            string plateTruck = "---------";
            string plateTrailer = "---------";
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.GetLastInspaction(token, idDriver, ref latsInspection, ref plateTruck, ref plateTrailer, ref description);
                });
                await PopupNavigation.PopAsync();
                if (state == 1)
                {
                    await PopupNavigation.PushAsync(new Errror("Not Network", null));
                }
                else if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    LatsInspection = latsInspection;
                    PlateTruck = plateTruck;
                    PlateTrailer = plateTrailer;
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }
    }
}