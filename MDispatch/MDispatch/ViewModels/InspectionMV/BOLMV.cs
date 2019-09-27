using System.Threading.Tasks;
using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV
{
    public class BOLMV : BindableBase
    {
        private BOLPage bOLPage = null;
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; } 
        public InitDasbordDelegate initDasbordDelegate = null;

        public BOLMV(ManagerDispatchMob managerDispatchMob, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate, BOLPage bOLPage)
        {
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            IdShip = idShip;
            this.initDasbordDelegate = initDasbordDelegate;
            this.bOLPage = bOLPage;
            InitShipping();
        }

        public string IdShip { get; set; }


        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private bool isLoad = false;
        public bool IsLoad
        {
            get => isLoad;
            set => SetProperty(ref isLoad, value);
        }


        private string email = null;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        [System.Obsolete]
        private async void InitShipping()
        {
            bool isNavigationMany = false;
            IsLoad = true;
            if (Navigation.NavigationStack.Count > 2)
            {
                isNavigationMany = true;
            }
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            Shipping shipping1 = null;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.GetShippingPhoto(token, IdShip, ref description, ref shipping1);
                });
                if (state == 2)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    await PopupNavigation.PushAsync(new Errror("Error", null));
                }
                else if (state == 3)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    Shipping = shipping1;
                    await bOLPage.InitPhoto(Shipping.VehiclwInformations);
                }
                else if (state == 4)
                {
                    if (isNavigationMany)
                    {
                        await PopupNavigation.RemovePageAsync(PopupNavigation.PopupStack[0]);
                        isNavigationMany = false;
                    }
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
                IsLoad = false;
            }
        }

        [System.Obsolete]
        public async Task SendLiabilityAndInsuranceEmaile()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.AskWork("SendBolMail", token, IdShip, Email, ref description);
                    initDasbordDelegate.Invoke();
                });
                if (state == 2)
                {
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    DependencyService.Get<IToast>().ShowMessage($"A copy of BOL is sent to the mail {Email}");
                }
                else if (state == 4)
                {
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
        }
    }
}