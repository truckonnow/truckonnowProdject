using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
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
        private object paymmpayMVInspactionant = null;

        public FeedBackMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, INavigation navigation, object paymmpayMVInspactionant)
        {
            this.paymmpayMVInspactionant = paymmpayMVInspactionant;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
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
                await PopupNavigation.PushAsync(new Errror("Not Network"));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description));
            }
            else if (state == 3)
            {
                await PopupNavigation.PopAsync(true);
                await PopupNavigation.PushAsync(new TempPageHint3());
                if(paymmpayMVInspactionant is AskForUsersDelyveryMW)
                {
                    if (((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "COD" || ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "COP" || ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "Biling")
                    {
                        await Navigation.PopToRootAsync(true);
                    }
                    else
                    {
                        //Camera
                    }
                }
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service"));
            }
        }
    }
}