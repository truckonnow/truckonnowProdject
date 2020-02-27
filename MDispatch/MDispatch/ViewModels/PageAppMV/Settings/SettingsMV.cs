using MDispatch.Service;
using Prism.Mvvm;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV.Settings
{
    public class SettingsMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;

        public SettingsMV(ManagerDispatchMob managerDispatchMob)
        {
            this.managerDispatchMob = managerDispatchMob;
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
    }
}