using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.AskPhoto;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.View.PageApp;
using MDispatch.View.PageApp.DialogPage;
using MDispatch.ViewModels.AskPhoto;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.Models;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
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
        private GetVechicleDelegate getVechicleDelegate = null;

        public InfoOrderMV(ManagerDispatchMob managerDispatchMob, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Shipping = shipping;
            getVechicleDelegate = GetVehiclwInformations;
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

        private List<VehiclwInformation> GetVehiclwInformations()
        {
            return Shipping.VehiclwInformations;
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

        public async void ToStartInspection()
        {
            foreach (var vehiclwInformation in Shipping.VehiclwInformations)
            {
                ICar car = null;
                if (vehiclwInformation.Ask != null && vehiclwInformation.Ask.TypeVehicle != null)
                {
                    car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                }
                List <PhotoInspection> photoInspections = null;
                if (vehiclwInformation.PhotoInspections != null)
                {
                    photoInspections = vehiclwInformation.PhotoInspections.FindAll(p => p.CurrentStatusPhoto == "PikedUp");
                }
                if (vehiclwInformation.Ask == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Start Inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new AskPage(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate), true);
                    return;
                }
                else if (photoInspections == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}1.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 1, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(1)), true);
                    return;
                }
                else if (photoInspections.Find(p => p.IndexPhoto == car.CountCarImg) == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    int lastIndexPhoto = photoInspections[vehiclwInformation.PhotoInspections.Count - 1].IndexPhoto + 1;
                    await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{lastIndexPhoto}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), lastIndexPhoto, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(lastIndexPhoto)), true);
                    return;
                }
                else if (vehiclwInformation.Ask1 == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new Ask1Page(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate, vehiclwInformation.Ask.TypeVehicle), true);
                    return;
                }
            }
            if (Shipping.VehiclwInformations[0].AskFromUser == null)
            {
                await Navigation.PushAsync(new AskForUser(managerDispatchMob, Shipping.VehiclwInformations[0], Shipping.Id, initDasbordDelegate), true);
                return;
            }
            else if (Shipping.VehiclwInformations[0].AskFromUser.App_will_ask_for_signature_of_the_client_signature == null)
            {
                await Navigation.PushAsync(new LiabilityAndInsurance(managerDispatchMob, Shipping.VehiclwInformations[0].Id, Shipping.Id, initDasbordDelegate), true);
                return;
            }
        }

        public async void ToStartInspectionDelyvery()
        {
            foreach (var vehiclwInformation in Shipping.VehiclwInformations)
            {
                ICar Car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                List<PhotoInspection> photoInspections = null;
                if (vehiclwInformation.PhotoInspections != null)
                {
                    photoInspections = vehiclwInformation.PhotoInspections.FindAll(p => p.CurrentStatusPhoto == "Delyvery");
                }
                if (vehiclwInformation.AskDelyvery == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Start Inspection Delyvered", vehiclwInformation));
                    await Navigation.PushAsync(new AskPageDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                    return;
                }
                else if (photoInspections != null && photoInspections.Count + 1 == 1)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Delyvered", vehiclwInformation));
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), photoInspections.Count + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(photoInspections.Count + 1)), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                    return;
                }
                else if (Car.GetIndexCarFullPhoto(photoInspections.Count+1) != 0)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Delyvered", vehiclwInformation));
                    int photoInspection = Car.GetIndexCarFullPhoto(photoInspections.Count + 1);
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), photoInspections.Count + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(photoInspections.Count + 1)), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier));
                    return;
                }
            }
            if (Shipping.VehiclwInformations[0].askForUserDelyveryM == null || Shipping.VehiclwInformations[0].askForUserDelyveryM.App_will_ask_for_name_of_the_client_signature == null)
            {
                await Navigation.PushAsync(new AskForUserDelyvery(managerDispatchMob, Shipping.VehiclwInformations[0], Shipping.Id, initDasbordDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                return;
            }
            //AskForUsersDelyveryMW askForUsersDelyveryMW = new AskForUsersDelyveryMW(managerDispatchMob, Shipping.VehiclwInformations[0], Shipping.Id, Navigation, initDasbordDelegate, Shipping.TotalPaymentToCarrier, Shipping.VehiclwInformations[0].askForUserDelyveryM.What_form_of_payment_are_you_using_to_pay_for_transportation);
        }

        private ICar GetTypeCar(string typeCar)
        {
            ICar car = null;
            switch (typeCar)
            {
                case "PickUp":
                    {
                        car = new CarPickUp();
                        break;
                    }
                case "Coupe":
                    {
                        car = new CarCoupe();
                        break;
                    }
                case "Suv":
                    {
                        car = new CarSuv();
                        break;
                    }
            }
            return car;
        }
    }
}