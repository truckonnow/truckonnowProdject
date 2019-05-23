using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
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

        private MDispatch.Models.Feedback feedback = null;
        public MDispatch.Models.Feedback Feedback
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

        private string email = null;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        [System.Obsolete]
        public async void SaveAsk()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    managerDispatchMob.AskWork("SendCouponMail", token, null, Email, ref description);
                    state = managerDispatchMob.AskWork("FeedBack", token, null, Feedback, ref description);
                });
                await PopupNavigation.PopAsync(true);
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    DependencyService.Get<IToast>().ShowMessage("Feedback saved");
                    await PopupNavigation.PushAsync(new TempPageHint3());
                    if (paymmpayMVInspactionant is AskForUsersDelyveryMW)
                    {
                        if (((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "COD" || ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "COP" || ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Payment == "Biling")
                        {
                            ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Continue();
                            await Navigation.PopToRootAsync(true);
                        }
                        else
                        {
                            await ((AskForUsersDelyveryMW)paymmpayMVInspactionant).Navigation.PushAsync(new CameraPaymmant(((AskForUsersDelyveryMW)paymmpayMVInspactionant), ""));
                        }
                    }
                    else
                    {
                        if (((LiabilityAndInsuranceMV)paymmpayMVInspactionant).What_form_of_payment_are_you_using_to_pay_for_transportation == "COD" || ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).What_form_of_payment_are_you_using_to_pay_for_transportation == "COP" || ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).What_form_of_payment_are_you_using_to_pay_for_transportation == "Biling")
                        {
                            ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).Continue();
                            await Navigation.PopToRootAsync(true);
                        }
                        else
                        {
                            await ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).Navigation.PushAsync(new CameraPaymmant(((LiabilityAndInsuranceMV)paymmpayMVInspactionant), ""));
                        }
                    }
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }
    }
}