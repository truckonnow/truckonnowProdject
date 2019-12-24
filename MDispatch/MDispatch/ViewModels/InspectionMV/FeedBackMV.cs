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
                    if (paymmpayMVInspactionant is AskForUsersDelyveryMW)
                    {
                        await Navigation.PopAsync(true);
                    }
                    else
                    {
                        if (((LiabilityAndInsuranceMV)paymmpayMVInspactionant).What_form_of_payment_are_you_using_to_pay_for_transportation == "Cash")
                        {
                            await Navigation.PushAsync(new VideoCameraPage(((LiabilityAndInsuranceMV)paymmpayMVInspactionant), ""));
                        }
                        else if (((LiabilityAndInsuranceMV)paymmpayMVInspactionant).What_form_of_payment_are_you_using_to_pay_for_transportation == "Check")
                        {
                            await Navigation.PushAsync(new CameraPaymmant(((LiabilityAndInsuranceMV)paymmpayMVInspactionant), "", "CheckPaymment.png"));
                        }
                        else
                        {
                            await Navigation.PushAsync(new Ask2Page(((LiabilityAndInsuranceMV)paymmpayMVInspactionant).managerDispatchMob, ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).IdVech, ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).IdShip, ((LiabilityAndInsuranceMV)paymmpayMVInspactionant).initDasbordDelegate));
                        }
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
