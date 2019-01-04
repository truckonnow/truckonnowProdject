using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.AskPhoto;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.View.PageApp;
using MDispatch.View.PageApp.DialogPage;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.PageAppMV
{
    public class InfoOrderMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;
        public DelegateCommand ToInstructionComand { get; set; }
        public DelegateCommand ToEditPikedUpCommand { get; set; }
        public DelegateCommand ToEditDeliveryCommand { get; set; }
        public DelegateCommand ToPaymentCommand { get; set; }
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public InfoOrderMV(ManagerDispatchMob managerDispatchMob, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            ToInstructionComand = new DelegateCommand(ToInstruction);
            ToEditPikedUpCommand = new DelegateCommand(ToEditPikedUp);
            ToEditDeliveryCommand = new DelegateCommand(ToEditDelivery);
            ToPaymentCommand = new DelegateCommand(ToEditPayment);
        }

        private Shipping shipping = null;
        public Shipping Shipping
        {
            get => shipping;
            set => SetProperty(ref shipping, value);
        }

        private int count = 0;
        public int Count
        {
            get => Shipping.VehiclwInformations.Count;
            set => SetProperty(ref count, value);
        }

        private void ToInstruction()
        {
            Navigation.PushAsync(new Instruction(this));
        }

        private async void ToEditPikedUp()
        {
            await Navigation.PushAsync(new EditPicupInfo(managerDispatchMob, Shipping), true);
        }

        private async void ToEditDelivery()
        {
            await Navigation.PushAsync(new EditDeliveryInformation(managerDispatchMob, Shipping), true);
        }

        private async void ToEditPayment()
        {
            await PopupNavigation.PushAsync(new EditPayment(managerDispatchMob, Shipping), true);
        }

        public async void ToVehicleDetails(VehiclwInformation vehiclwInformation)
        {
            await Navigation.PushAsync(new VechicleDetails(vehiclwInformation, managerDispatchMob));
        }

        public async void ToStartInspection(VehiclwInformation vehiclwInformation, Shipping shipping)
        {
            if (vehiclwInformation.Ask == null)
            {
                await Navigation.PushAsync(new AskPage(managerDispatchMob, vehiclwInformation, shipping, initDasbordDelegate), true);
            }
            else if(vehiclwInformation.PhotoInspections == null)
            {
                await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, shipping, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}1.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 1, initDasbordDelegate), true);
            }
            else if(vehiclwInformation.PhotoInspections.Find(p => p.IndexPhoto == 39) == null)
            {
                int lastIndexPhoto = vehiclwInformation.PhotoInspections[vehiclwInformation.PhotoInspections.Count - 1].IndexPhoto + 1;
                 await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, shipping, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{lastIndexPhoto}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), lastIndexPhoto, initDasbordDelegate), true);
            }
            else if(vehiclwInformation.Ask1 == null)
            {
                await Navigation.PushAsync(new Ask1Page(managerDispatchMob, vehiclwInformation, shipping, initDasbordDelegate), true);
            }
            else if(vehiclwInformation.AskFromUser == null)
            {
                await Navigation.PushAsync(new AskForUser(managerDispatchMob, vehiclwInformation, shipping, initDasbordDelegate), true);
            }

        }
    }
}