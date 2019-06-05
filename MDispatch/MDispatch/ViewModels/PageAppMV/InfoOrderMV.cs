using MDispatch.Models;
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
using System.Linq;
using static MDispatch.Service.ManagerDispatchMob;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using MDispatch.ViewModels.InspectionMV.Servise.Models;

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

        [System.Obsolete]
        private async void ToEditPayment()
        {
            await PopupNavigation.PushAsync(new EditPayment(managerDispatchMob, Shipping), true);
        }

        public async void ToVehicleDetails(VehiclwInformation vehiclwInformation)
        {
            await Navigation.PushAsync(new VechicleDetails(vehiclwInformation, managerDispatchMob));
        }

        [System.Obsolete]
        public async void ToStartInspection()
        {
            VehiclwInformation vehiclwInformation1 = null;
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
                    await Navigation.PushAsync(new AskPage(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                    return;
                }
                else if (photoInspections == null || photoInspections.Count == 0)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    FullPagePhoto fullPagePhoto = new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}1.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), 1, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(1), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier);
                    await Navigation.PushAsync(fullPagePhoto, true);
                    await Navigation.PushAsync(new CameraPagePhoto($"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}1.png", fullPagePhoto));
                    return;
                }
                else if (photoInspections.Find(p => p.IndexPhoto == car.CountCarImg) == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    int lastIndexPhoto = photoInspections[vehiclwInformation.PhotoInspections.Count - 1].IndexPhoto + 1;
                    FullPagePhoto fullPagePhoto = new FullPagePhoto(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{lastIndexPhoto}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), lastIndexPhoto, initDasbordDelegate, getVechicleDelegate, car.GetNameLayout(lastIndexPhoto), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier);
                    await Navigation.PushAsync(fullPagePhoto, true);
                    await Navigation.PushAsync(new CameraPagePhoto($"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{lastIndexPhoto}.png", fullPagePhoto));
                    return;
                }
                else if (vehiclwInformation.Ask1 == null)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Picked up", vehiclwInformation));
                    await Navigation.PushAsync(new Ask1Page(managerDispatchMob, vehiclwInformation, Shipping.Id, initDasbordDelegate, getVechicleDelegate, vehiclwInformation.Ask.TypeVehicle, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                    return;
                }
            }
            vehiclwInformation1 = Shipping.VehiclwInformations.FirstOrDefault(v => v.AskFromUser != null);
            if (vehiclwInformation1 == null)
            {
                await Navigation.PushAsync(new AskForUser(managerDispatchMob, vehiclwInformation1, Shipping.Id, initDasbordDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                return;
            }
            else if (vehiclwInformation1.AskFromUser.App_will_ask_for_signature_of_the_client_signature == null || vehiclwInformation1.AskFromUser.What_form_of_payment_are_you_using_to_pay_for_transportation == null || vehiclwInformation1.AskFromUser.CountPay == null)
            {
                await Navigation.PushAsync(new LiabilityAndInsurance(managerDispatchMob, vehiclwInformation1.Id, Shipping.Id, initDasbordDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                return;
            }
            else if(vehiclwInformation1 != null && vehiclwInformation1.AskFromUser.PhotoPay == null)
            {
                LiabilityAndInsuranceMV askForUsersDelyveryMW = new LiabilityAndInsuranceMV(managerDispatchMob, vehiclwInformation1.Id, Shipping.Id, Navigation, initDasbordDelegate);
                await Navigation.PushAsync(new CameraPaymmant(askForUsersDelyveryMW, ""));
            }
        }

        [System.Obsolete]
        public async void ToStartInspectionDelyvery()
        {
            VehiclwInformation vehiclwInformation1 = null;
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
                    FullPagePhotoDelyvery fullPagePhotoDelyvery = new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), photoInspections.Count + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(photoInspections.Count + 1)), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier);
                    await Navigation.PushAsync(fullPagePhotoDelyvery, true);
                    await Navigation.PushAsync(new CameraPagePhoto1($"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", fullPagePhotoDelyvery));
                    return;
                }
                else if (Car.GetIndexCarFullPhoto(photoInspections.Count+1) != 0)
                {
                    await PopupNavigation.PushAsync(new HintPageVechicle("Continuing inspection Delyvered", vehiclwInformation));
                    int photoInspection = Car.GetIndexCarFullPhoto(photoInspections.Count + 1);
                    FullPagePhotoDelyvery fullPagePhotoDelyvery = new FullPagePhotoDelyvery(managerDispatchMob, vehiclwInformation, Shipping.Id, $"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", vehiclwInformation.Ask.TypeVehicle.Replace(" ", ""), photoInspections.Count + 1, initDasbordDelegate, getVechicleDelegate, Car.GetNameLayout(Car.GetIndexCarFullPhoto(photoInspections.Count + 1)), Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier);
                    await Navigation.PushAsync(fullPagePhotoDelyvery);
                    await Navigation.PushAsync(new CameraPagePhoto1($"{vehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}{Car.GetIndexCarFullPhoto(photoInspections.Count + 1)}.png", fullPagePhotoDelyvery));
                    return;
                }
            }
            vehiclwInformation1 = Shipping.VehiclwInformations.FirstOrDefault(v => v.askForUserDelyveryM != null);
            if (vehiclwInformation1 == null || vehiclwInformation1.askForUserDelyveryM.App_will_ask_for_name_of_the_client_signature == null)
            {
                await Navigation.PushAsync(new AskForUserDelyvery(managerDispatchMob, vehiclwInformation1, Shipping.Id, initDasbordDelegate, Shipping.OnDeliveryToCarrier, Shipping.TotalPaymentToCarrier), true);
                return;
            }
            else if (vehiclwInformation1 != null && vehiclwInformation1.askForUserDelyveryM.PhotoPay == null)
            {
                AskForUsersDelyveryMW askForUsersDelyveryMW = new AskForUsersDelyveryMW(managerDispatchMob, vehiclwInformation1, Shipping.Id, Navigation, initDasbordDelegate, Shipping.TotalPaymentToCarrier, vehiclwInformation1.askForUserDelyveryM.What_form_of_payment_are_you_using_to_pay_for_transportation);
                await Navigation.PushAsync(new CameraPaymmant(askForUsersDelyveryMW, ""));
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
                case "Suv":
                    {
                        car = new CarSuv();
                        break;
                    }
                case "Sedan":
                    {
                        car = new CarSedan();
                        break;
                    }
            }
            return car;
        }
    }
}