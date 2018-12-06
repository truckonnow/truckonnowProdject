using MDispatch.Models;
using MDispatch.Service;
using Plugin.Settings;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.ViewModels.PageAppMV
{
    public class FullPagePhotoMV : BindableBase
    {
        public ManagerDispatchMob managerDispatchMob = null;
        public INavigation Navigationn { get; set; }

        public FullPagePhotoMV(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping)
        {
            this.managerDispatchMob = managerDispatchMob;
            VehiclwInformation = vehiclwInformation;
            InitPhoto(VehiclwInformation.Photos);
        }
        
        private VehiclwInformation vehiclwInformation = null;
        public VehiclwInformation VehiclwInformation
        {
            get => vehiclwInformation;
            set => SetProperty(ref vehiclwInformation, value);
        }

        private ImageSource sourseImage = null;
        public ImageSource SourseImage
        {
            get => sourseImage;
            set => SetProperty(ref sourseImage, value);
        }

        private List<ImageSource> allSourseImage = null;
        public List<ImageSource> AllSourseImage
        {
            get => allSourseImage;
            set => SetProperty(ref allSourseImage, value);
        }

        private void InitPhoto(List<Models.Photo> photos)
        {
            if (photos != null)
            {
                List<ImageSource> allSourseImage1 = new List<ImageSource>();
                AllSourseImage = new List<ImageSource>();
                foreach (var photo in photos)
                {
                    allSourseImage1.Add(ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(photo.Base64))));
                }
                AllSourseImage = allSourseImage1;
                SourseImage = AllSourseImage[0];
            }
        }

        public void AddNewFotoSourse(byte[] imageSorseByte)
        {
            if(AllSourseImage == null)
            {
                AllSourseImage = new List<ImageSource>();
            }
            List<ImageSource> imageSources1 = new List<ImageSource>(AllSourseImage);
            imageSources1.Add(ImageSource.FromStream(() => new MemoryStream(imageSorseByte)));
            AllSourseImage = imageSources1;
        }

        public async void SetPhoto(byte[] PhotoInArrayByte)
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            await Task.Run(() =>
            {
                state = managerDispatchMob.PhotoWork("SavePhoto", token, VehiclwInformation.Id, PhotoInArrayByte, ref description);
            });
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}
