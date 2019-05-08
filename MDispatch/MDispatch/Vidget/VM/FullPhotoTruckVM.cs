using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.Vidget.VM
{
    public class FullPhotoTruckVM : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;
        private INavigation navigation = null;
        public TruckCar truckCar = null;
        public DelegateCommand NextCommand { get; set; }

        public FullPhotoTruckVM(ManagerDispatchMob managerDispatchMob, INavigation navigation, string idDriver, int indexCurent)
        {
            this.managerDispatchMob = managerDispatchMob;
            this.navigation = navigation;
            truckCar = new TruckCar();
            IdDriver = idDriver;
            IndexCurent = indexCurent;
            NextCommand = new DelegateCommand(NextPage);
            NameLayute = truckCar.GetNameTruck(IndexCurent);
            truckCar.GetModalAlert(IndexCurent);
            truckCar.Orinteble(IndexCurent);
        }

        public string IdDriver { get; set; }

        private int IndexCurent { get; set; }

        private bool isVisableNext = false;
        public bool IsVisableNext
        {
            get => isVisableNext;
            set => SetProperty(ref isVisableNext, value);
        }

        private bool isVisableAdd = true;
        public bool IsVisableAdd
        {
            get => isVisableAdd;
            set => SetProperty(ref isVisableAdd, value);
        }

        private string nameLayute = null;
        public string NameLayute
        {
            get => nameLayute;
            set => SetProperty(ref nameLayute, value);
        }

        private ImageSource source = null;
        public ImageSource Source
        {
            get => source;
            set => SetProperty(ref source, value);
        }

        private ImageSource imageSource = null;
        public ImageSource ImageSource
        { 
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        private ImageSource imageSourceTake = null;
        public ImageSource ImageSourceTake
        {
            get => imageSourceTake;
            set => SetProperty(ref imageSourceTake, value);
        }

        public InspectionDriver InspectionDriver { get; set; }

        public void AddPhoto(byte[] photoRes)
        {
            if(InspectionDriver == null)
            {
                InspectionDriver = new InspectionDriver();
                InspectionDriver.PhotosTruck = new List<Photo>();
            }
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(photoRes);
            photo.path = $"../Photo/Driver/{IdDriver}/{IndexCurent}.jpg";
            InspectionDriver.PhotosTruck.Add(photo);
            Stream stream = new MemoryStream(photoRes);
            ImageSourceTake = ImageSource.FromStream(() => { return stream; });
            IsVisableAdd = false;
            IsVisableNext = true;
        }

        private void NextPage()
        {
            if (IndexCurent == 45)
            {

            }
            else
            {
                IndexCurent++;
                NameLayute = truckCar.GetNameTruck(IndexCurent);
                truckCar.GetModalAlert(IndexCurent);
                truckCar.Orinteble(IndexCurent);
                ImageSourceTake = null;
                IsVisableAdd = true;
                IsVisableNext = false;
                Source = null;
            }
        }

        [System.Obsolete]
        private async void Init()
        {
            await PopupNavigation.PushAsync(new LoadPage(), true);
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            VehiclwInformation vehiclwInformation1 = null;
            await Task.Run(() =>
            {
                //state = managerDispatchMob.OrderWork("GetVechicleInffo", idVech, ref vehiclwInformation1, token, ref description);
            });
            await PopupNavigation.PopAsync(true);
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", null));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, null));
            }
            else if (state == 3)
            {
                
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", null));
            }
        }
    }
}
