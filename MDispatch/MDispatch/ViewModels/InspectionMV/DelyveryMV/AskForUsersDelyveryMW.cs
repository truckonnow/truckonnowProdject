using MDispatch.Models;
using MDispatch.NewElement.ToastNotify;
using MDispatch.Service;
using MDispatch.View;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection.Delyvery;
using Newtonsoft.Json;
using Plugin.Settings;
using Prism.Mvvm;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.ViewModels.InspectionMV.DelyveryMV
{
    public class AskForUsersDelyveryMW : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigation { get; set; }
        private InitDasbordDelegate initDasbordDelegate = null;

        public AskForUsersDelyveryMW(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, INavigation navigation, InitDasbordDelegate initDasbordDelegate, 
            string totalPaymentToCarrier, string paymmant = null)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.managerDispatchMob = managerDispatchMob;
            Navigation = navigation;
            VehiclwInformation = vehiclwInformation;
            IdShip = idShip;
            TotalPaymentToCarrier = totalPaymentToCarrier;
            if(paymmant != null)
            {
                Payment = paymmant;
            }
        }

        private string IdShip { get; set; }
        private string TotalPaymentToCarrier { get; set; }
        public string Payment { get; set; }

        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        private AskForUserDelyveryM askForUserDelyveryM = null;
        public AskForUserDelyveryM AskForUserDelyveryM
        {
            get => askForUserDelyveryM;
            set => SetProperty(ref askForUserDelyveryM, value);
        }

        private string email = null;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private int inderxPhotoInspektion = 0;
        public int InderxPhotoInspektion
        {
            get => inderxPhotoInspektion;
            set => SetProperty(ref inderxPhotoInspektion, value);
        }

        public List<DamageForUser> damageForUsers { get; set; }

        public async void SaveAsk(string paymmant)
        {
            Payment = paymmant;
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await PopupNavigation.PushAsync(new TempDialogPage1(this));
            await Task.Run(() =>
            {
                Task.Run(() =>
                {
                    managerDispatchMob.AskWork("DamageForUser", token, vehiclwInformation.Id, damageForUsers, ref description);
                });
                state = managerDispatchMob.AskWork("AskForUserDelyvery", token, VehiclwInformation.Id, AskForUserDelyveryM, ref description);
                initDasbordDelegate.Invoke();
            });
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", Navigation));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, Navigation));
            }
            else if (state == 3)
            {
                DependencyService.Get<IToast>().ShowMessage("Answers to questions saved");
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
            }
        }

        public async void AddPhoto(byte[] photoResult)
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            Photo photo = new Photo();
            photo.Base64 = JsonConvert.SerializeObject(photoResult);
            photo.path = $"../Photo/{VehiclwInformation.Id}/Pay/DelyverySig.jpg";
            await Navigation.PopToRootAsync();
            await Task.Run(() =>
            {
                Continue();
            });
            await Task.Run(() =>
            {
                state = managerDispatchMob.SavePay(token, VehiclwInformation.Id, 2, photo, ref description);
                initDasbordDelegate.Invoke();
            });
            if (state == 1)
            {
                await PopupNavigation.PushAsync(new Errror("Not Network", Navigation));
            }
            else if (state == 2)
            {
                await PopupNavigation.PushAsync(new Errror(description, Navigation));
            }
            else if (state == 3)
            {
                DependencyService.Get<IToast>().ShowMessage("Payment method photo saved");
            }
            else if (state == 4)
            {
                await PopupNavigation.PushAsync(new Errror("Technical work on the service", Navigation));
            }
        }

        public void RemmoveDamage(Image image, StackLayout stackLayout)
        {
            if (image != null && damageForUsers != null && damageForUsers.FirstOrDefault(d => d.Image == image) != null)
            {
                List<ImageSource> AllSourseImage = new List<ImageSource>();
                stackLayout.Children.ToList().ForEach((imageV) => 
                {
                    Image tempImage = (Image)imageV;
                    AllSourseImage.Add(tempImage.Source);
                });
                List<ImageSource> imageSources2 = new List<ImageSource>(AllSourseImage);
                DamageForUser damageForUser = damageForUsers.FirstOrDefault(d => d.Image == image);
                imageSources2.Remove(imageSources2.FirstOrDefault(i => i == damageForUser.ImageSource));
                AllSourseImage = imageSources2;
                damageForUsers.Remove(damageForUser);
                stackLayout.Children.Remove(image);
            }
        }

        internal void SetDamage(string nameDamage, int indexDamage, string prefNameDamage, double xInterest, double yInterest, Image image, ImageSource imageSource1)
        {
            DamageForUser damageForUser = new DamageForUser();
            damageForUser.FullNameDamage = $"{prefNameDamage} - {nameDamage}";
            damageForUser.TypePrefDamage = prefNameDamage;
            damageForUser.IndexDamage = indexDamage;
            damageForUser.XInterest = xInterest;
            damageForUser.YInterest = yInterest;
            damageForUser.Image = image;
            damageForUser.TypeCurrentStatus = "D";
            damageForUser.ImageSource = imageSource1;
            if (damageForUsers == null)
            {
                damageForUsers = new List<DamageForUser>();
            }
            damageForUsers.Add(damageForUser);
        }

        public async void Continue()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                string status = null;
                if(TotalPaymentToCarrier == "COD" && TotalPaymentToCarrier == "COP")
                {
                    status = "Delivered,Paid";
                }
                else
                {
                    status = "Delivered,Billed";
                }
                state = managerDispatchMob.Recurent(token, IdShip, status, ref description);
                initDasbordDelegate.Invoke();
            });
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

        public async void GoToFeedBack()
        {
            await PopupNavigation.PopAllAsync(true);
            await Navigation.PushAsync(new View.Inspection.Feedback(managerDispatchMob, VehiclwInformation, this));
        }

        public void SendEmailCoupon()
        {
            //To Do
        }
    }
}