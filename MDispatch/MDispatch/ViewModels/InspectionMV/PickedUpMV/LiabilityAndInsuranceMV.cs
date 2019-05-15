using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection.PickedUp;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.PickedUpMV
{
    public class LiabilityAndInsuranceMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        public DelegateCommand SaveLiabilityAndInsuranceCommand { get; set; }
        public DelegateCommand GoToFeedBackCommand { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public LiabilityAndInsuranceMV(ManagerDispatchMob managerDispatchMob, string idVech, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate)
        {
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            IdShip = idShip;
            IdVech = idVech;
            GoToFeedBackCommand = new DelegateCommand(GoToFeedBack);
            this.initDasbordDelegate = initDasbordDelegate;
            SaveLiabilityAndInsuranceCommand = new DelegateCommand(SendLiabilityAndInsuranceEmaile);
            InitShipping();
        }
        private string email = null;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string IdShip { get; set; }

        public string IdVech { get; set; }

        public int StataLoadShip { get; set; }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private Photo sigPhoto = null;
        public Photo SigPhoto
        {
            get => sigPhoto;
            set => SetProperty(ref sigPhoto, value);
        }

        public string What_form_of_payment_are_you_using_to_pay_for_transportation { set; get; }
        public string CountPay { set; get; }

        [System.Obsolete]
        private async void InitShipping()
        {
            bool isNavigationMany = false;
            if (Navigation.NavigationStack.Count > 3)
            {
                await PopupNavigation.PushAsync(new LoadPage());
                isNavigationMany = true;
            }
            await PopupNavigation.PushAsync(new LoadPage());
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            Shipping shipping1 = null;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.GetShipping(token, IdShip, ref description, ref shipping1);
                });
                await PopupNavigation.PopAsync();
                if (state == 2)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    Shipping = shipping1;
                }
                else if (state == 4)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    if (Navigation.NavigationStack.Count > 1)
                    {
                        await Navigation.PopAsync();
                    }
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
                StataLoadShip = 1;
            }
        }

        [System.Obsolete]
        public async void AddPhoto(byte[] photoResult)
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                Photo photo = new Photo();
                photo.Base64 = JsonConvert.SerializeObject(photoResult);
                photo.path = $"../Photo/{IdVech}/Pay/DelyverySig.jpg";
                await Navigation.PopToRootAsync();
                await Task.Run(() =>
                {
                    Continue();
                });
                await Task.Run(() =>
                {
                    state = managerDispatchMob.SavePay(token, IdVech, 1, photo, ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    DependencyService.Get<IToast>().ShowMessage("Paymmant photo saved");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
        }

        [System.Obsolete]
        public async void SaveSigAndMethodPay()
        {
            await PopupNavigation.PushAsync(new LoadPage());
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("AskPikedUpSig", token, IdVech, SigPhoto, ref description);
                    state = managerDispatchMob.SaveMethodPay(token, IdVech, What_form_of_payment_are_you_using_to_pay_for_transportation, CountPay, ref description);
                    initDasbordDelegate.Invoke();
                });
                await PopupNavigation.PopAsync();
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, Navigation));
                }
                else if (state == 3)
                {
                    await PopupNavigation.PushAsync(new CopyLibaryAndInsurance(this));
                    DependencyService.Get<IToast>().ShowMessage("Paymmant method saved");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
                }
            }
        }

        [System.Obsolete]
        public async void Continue()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.Recurent(token, IdShip, "Picked up", ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }

        private async void SendLiabilityAndInsuranceEmaile()
        {
            GoEvaluationAndSurvey();
            //SendOnEmail
        }

        [System.Obsolete]
        public async void GoEvaluationAndSurvey()
        {
            await PopupNavigation.PopAsync(true);
            await PopupNavigation.PushAsync(new EvaluationAndSurveyDialog(this, Navigation));
        }

        [System.Obsolete]
        private async void GoToFeedBack()
        {
            await PopupNavigation.PopAllAsync(true);
            await Navigation.PushAsync(new View.Inspection.Feedback(managerDispatchMob, Shipping.VehiclwInformations.FirstOrDefault(v => v.Id == IdVech), this));
        }
    }
}