using MDispatch.Models;
using MDispatch.Service;
using MDispatch.Service.Net;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.PageApp;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV.VehicleDetals
{
    public class VehicleDetailsMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigationn { get; set; }
        private VechicleDetails vechicleDetails = null;

        public VehicleDetailsMV(ManagerDispatchMob managerDispatchMob, int idVech, VechicleDetails vechicleDetails)
        {
            this.managerDispatchMob = managerDispatchMob;
            VehiclwInformation = vehiclwInformation;
            this.vechicleDetails = vechicleDetails;
            InitVehiclwInformation(idVech);
        }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        [System.Obsolete]
        private async void InitVehiclwInformation(int idVech)
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            VehiclwInformation vehiclwInformation1 = null;
            await Task.Run(() => Utils.CheckNet());
            if (App.isNetwork)
            {
                await Task.Run(() =>
                {
                    state = managerDispatchMob.OrderWork("GetVechicleInffo", idVech, ref vehiclwInformation1, token, ref description);
                });
                await PopupNavigation.PopAsync(true);
                if (state == 2)
                {
                    await Navigationn.PopAsync(true);
                    await PopupNavigation.PushAsync(new Errror(description, null));
                }
                else if (state == 3)
                {
                    VehiclwInformation = vehiclwInformation1;
                    await vechicleDetails.InitPhoto(VehiclwInformation);
                }
                else if (state == 4)
                {
                    await Navigationn.PopAsync(true);
                    await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
                }
            }
            else
            {
                await Navigationn.PopAsync(true);
                await PopupNavigation.PopAsync(true);
            }
        }
    }
}