using MDispatch.Models;
using MDispatch.NewElement;
using MDispatch.Service;
using MDispatch.View.AskPhoto;
using MDispatch.View.Inspection;
using MDispatch.View.Inspection.Delyvery;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.View.PageApp;
using MDispatch.View.PageApp.DialogPage;
using MDispatch.ViewModels.AskPhoto;
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
                    ICar car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}1.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 1, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(1)), true);
                    return;
                }
                else if (photoInspections.Find(p => p.IndexPhoto == 39) == null)
                {
                    ICar car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    int lastIndexPhoto = photoInspections[vehiclwInformation.PhotoInspections.Count - 1].IndexPhoto + 1;
                    await Navigation.PushAsync(new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{lastIndexPhoto}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), lastIndexPhoto, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(lastIndexPhoto)), true);
                    return;
                }
                else if (vehiclwInformation.Ask1 == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new Ask1Page(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate), true);
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
                List<PhotoInspection> photoInspections = null;
                if (vehiclwInformation.PhotoInspections != null)
                {
                    photoInspections = vehiclwInformation.PhotoInspections.FindAll(p => p.CurrentStatusPhoto == "Delyvery");
                }
                if (vehiclwInformation.AskDelyvery == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Start Inspection Delyvered", vehiclwInformation));
                    await Navigation.PushAsync(new AskPageDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate), true);
                    return;
                }
                else if (photoInspections == null || photoInspections.Count == 0)
                {
                    ICar Car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Delyvered", vehiclwInformation));
                    await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}7.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 7, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(7)), true);
                    return;
                }
                else if (photoInspections.Count < 7 && photoInspections[photoInspections.Count - 1].IndexPhoto != 20)
                {
                    ICar Car = GetTypeCar(vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""));
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Delyvered", vehiclwInformation));
                    PhotoInspection photoInspection = photoInspections[photoInspections.Count - 1];
                    if (photoInspection.IndexPhoto == 7)
                    {
                        await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}19.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 19, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(19)));
                    }
                    else if (photoInspection.IndexPhoto == 19)
                    {
                        await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}2.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 2, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(2)));
                    }
                    else if (photoInspection.IndexPhoto == 2)
                    {
                        await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}16.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 16, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(16)));
                    }
                    else if (photoInspection.IndexPhoto == 16)
                    {
                        await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}23.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 23, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(23)));
                    }
                    else if (photoInspection.IndexPhoto == 23)
                    {
                        await Navigation.PushAsync(new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}20.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 20, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(20)));
                    }
                    return;
                }
            }
            if (Shipping.VehiclwInformations[0].askForUserDelyveryM == null || Shipping.VehiclwInformations[0].askForUserDelyveryM.App_will_ask_for_name_of_the_client_signature == null)
            {
                await Navigation.PushAsync(new AskForUserDelyvery(managerDispatchMob, Shipping.VehiclwInformations[0], Shipping.Id, initDasbordDelegate), true);
                return;
            }
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
            }
            return car;
        }
    }
}