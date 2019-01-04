using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.Inspection.PickedUp;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.InspectionMV
{
    public class FeedBackMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }

        public FeedBackMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, INavigation navigation)
        {
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            Shipping = shipping;
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private Feedback feedback = null;
        public Feedback Feedback
        {
            get => feedback;
            set => SetProperty(ref feedback, value);
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        public async void SaveAsk()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.AskWork("FeedBack", token, null, Feedback, ref description);
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                await PopupNavigation.PushAsync(new TempPageHint3());
                await Navigation.PopToRootAsync(true);
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}