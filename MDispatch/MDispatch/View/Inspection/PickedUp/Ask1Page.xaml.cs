﻿using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.Inspection.CameraPageFolder;
using MDispatch.View.Inspection.PickUp.CameraPageFolder;
using MDispatch.ViewModels.InspectionMV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ask1Page : ContentPage
	{
        public Ask1PageMV ask1PageMV = null;
        private Ask1 Ask1 = null;

        public Ask1Page(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
        {
            ask1PageMV = new Ask1PageMV(managerDispatchMob, vehiclwInformation, shipping, Navigation, initDasbordDelegate);
            Ask1 = new Ask1();
            InitializeComponent();
            BindingContext = ask1PageMV;
        }

        #region Ask1
        bool isAsk1 = false;
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk1 = true;
            }
            else
            {
                isAsk1 = false;
            }
            Ask1.Exact_Mileage = e.NewTextValue;
        }
        #endregion

        #region Ask2
        bool isAsk2 = false;
        private void Entry_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk2 = true;
            }
            else
            {
                isAsk2 = false;
            }
            Ask1.Did_you_notice_any_mechanical_imperfections_wile_loading = e.NewTextValue;
        }
        #endregion

        #region Ask3
        Button button3 = null;
        bool isAsk3 = false;
        private void Button_Clicked(object sender, EventArgs e)
        {
            isAsk3 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask1.Did_someone_help_you_load_it = button.Text;
            if (button3 != null)
            {
                button3.TextColor = Color.Silver;
            }
            button3 = button;
        }
        #endregion

        #region Ask4
        bool isAsk4 = false;
        private void Entry_TextChanged2(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk4 = true;
            }
            else
            {
                isAsk4 = false;
            }
            Ask1.Did_someone_load_the_vehicle_for_you = e.NewTextValue;
        }
        #endregion

        #region Ask5
        bool isAsk5 = false;
        private void Entry_TextChanged3(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk5 = true;
            }
            else
            {
                isAsk5 = false;
            }
            Ask1.Did_someone_load_the_vehicle_for_you = e.NewTextValue;
        }
        #endregion

        #region Ask6
        bool isAsk6 = false;
        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk6 = true;
            Ask1.What_method_of_exit_did_you_use = (string)e.NewItem;
        }
        #endregion

        #region Ask7
        Button button1 = null;
        bool isAsk7 = false;
        private void Button_Clicked1(object sender, EventArgs e)
        {
            isAsk7 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask1.Did_you_jumped_the_vehicle_to_start = button.Text;
            if (button1 != null)
            {
                button1.TextColor = Color.Silver;
            }
            button1 = button;
        }
        #endregion

        #region Ask8
        Button button2 = null;
        bool isAsk8 = false;
        private void Button_Clicked2(object sender, EventArgs e)
        {
            isAsk8 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask1.Have_you_used_winch = button.Text;
            if (button2 != null)
            {
                button2.TextColor = Color.Silver;
            }
            button2 = button;
        }
        #endregion

        #region Ask9
        bool isAsk9 = false;
        private void Entry_TextChanged4(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk9 = true;
            }
            else
            {
                isAsk9 = false;
            }
            Ask1.How_many_keys_total_you_been_given = e.NewTextValue;
        }
        #endregion

        #region Ask10
        Button button4 = null;
        bool isAsk10 = false;
        private void Button_Clicked3(object sender, EventArgs e)
        {
            isAsk10 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask1.All_4_wheels_are_correctly_strapped_strapped = button.Text;
            if (button4 != null)
            {
                button4.TextColor = Color.Silver;
            }
            button4 = button;
        }
        #endregion

        #region Ask11
        bool isAsk11 = false;
        private void Dropdown_SelectedItemChanged1(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk11 = true;
            Ask1.What_method_of_exit_did_you_use = (string)e.NewItem;
        }
        #endregion

        #region Ask12
        bool isAsk12 = false;
        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraSeatBelts(this));
        }

        public void AddPhotoSeatBelts(List<Photo> photos, List<byte[]> imagesByte)
        {
            if(photos != null && photos.Count == 4)
            {
                isAsk12 = true;
                Ask1.App_will_force_driver_to_take_pictures_of_each_strap = new List<Photo>(photos);
                foreach (var imageByte in imagesByte)
                {
                    blockAskPhotoSeatBelts.Children.Add(new Image()
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(imageByte)),
                        HeightRequest = 50,
                        WidthRequest = 50
                    });
                }
            }
            else
            {
                isAsk12 = false;
            }
        }
        #endregion

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk2 && isAsk3 && isAsk4 && isAsk5 && isAsk6 && isAsk7 && isAsk8 && isAsk9 && isAsk10 && isAsk11)
            {
                ask1PageMV.Ask1 = Ask1;
                ask1PageMV.SaveAsk();
            }
            else
            {
                CheckAsk();
            }
        }

        private void CheckAsk()
        {
            if (!isAsk1)
            {
                askBlock1.BorderColor = Color.Red;
            }
            if (!isAsk2)
            {
                askBlock2.BorderColor = Color.Red;
            }
            if (!isAsk3)
            {
                askBlock3.BorderColor = Color.Red;
            }
            if (!isAsk4)
            {
                askBlock4.BorderColor = Color.Red;
            }
            if (!isAsk5)
            {
                askBlock5.BorderColor = Color.Red;
            }
            if (!isAsk6)
            {
                askBlock6.BorderColor = Color.Red;
            }
            if (!isAsk7)
            {
                askBlock7.BorderColor = Color.Red;
            }
            if (!isAsk8)
            {
                askBlock8.BorderColor = Color.Red;
            }
            if (!isAsk9)
            {
                askBlock9.BorderColor = Color.Red;
            }
            if (!isAsk10)
            {
                askBlock10.BorderColor = Color.Red;
            }
            if (!isAsk11)
            {
                askBlock11.BorderColor = Color.Red;
            }
            if(!isAsk12)
            {
                askBlock12.BorderColor = Color.Red;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraSpareParts(this));
        }

        public void AddPhotoSpareParts(byte[] photob)
        {
            if (Ask1.Any_additional_parts_been_given_to_you == null)
            {
                Ask1.Any_additional_parts_been_given_to_you = new List<Models.Photo>();
            }
            Models.Photo photo = new Models.Photo();
            photo.Base64 = JsonConvert.SerializeObject(photob);
            photo.path = $"Photo/PikedUp/{ask1PageMV.VehiclwInformation.Id}/Ask/SpareParts/{ Ask1.Any_additional_parts_been_given_to_you.Count + 1}.png";
            Ask1.Any_additional_parts_been_given_to_you.Add(photo);
            blockAskPhotoSpareParts.Children.Add(new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photob)),
                HeightRequest = 50,
                WidthRequest = 50
            });
        }

        public void AddPhotoDocumentations(byte[] photob)
        {
            if (Ask1.Any_additional_documentation_been_given_after_loading == null)
            {
                Ask1.Any_additional_documentation_been_given_after_loading = new List<Models.Photo>();
            }
            Models.Photo photo = new Models.Photo();
            photo.Base64 = JsonConvert.SerializeObject(photob);
            photo.path = $"Photo/PikedUp/{ask1PageMV.VehiclwInformation.Id}/Ask/Documentations/{ Ask1.Any_additional_documentation_been_given_after_loading.Count + 1}.png";
            Ask1.Any_additional_documentation_been_given_after_loading.Add(photo);
            blockAskPhotoDocumentations.Children.Add(new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photob)),
                HeightRequest = 50,
                WidthRequest = 50
            });
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CameraDocumentations(this));
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {

        }
    }
}