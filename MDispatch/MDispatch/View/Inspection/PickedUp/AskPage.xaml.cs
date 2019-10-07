using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.AskPhoto.CameraPageFolder;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection;
using MDispatch.ViewModels.AskPhoto;
using Newtonsoft.Json;
using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.AskPhoto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AskPage : ContentPage
    {
        AskPageMV askPageMV = null;

        public AskPage(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate,
            string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            askPageMV = new AskPageMV(managerDispatchMob, vehiclwInformation, idShip, Navigation, initDasbordDelegate, getVechicleDelegate, onDeliveryToCarrier, totalPaymentToCarrier);
            askPageMV.Ask = new Ask();
            InitializeComponent();
            BindingContext = askPageMV;
        }


        #region Ask1
        Button button1 = null;
        bool isAsk1 = false;
        private void Button_Clicked(object sender, EventArgs e)
        {
            isAsk1 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askPageMV.Ask.Lightbrightness = button.Text;
            if (button1 != null)
            {
                button1.TextColor = Color.Silver;
            }
            button1 = button;
        }
        #endregion

        #region Ask2
        Button button2 = null;
        bool isAsk2 = false;
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            isAsk2 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askPageMV.Ask.Vehicle = button.Text;
            if (button2 != null)
            {
                button2.TextColor = Color.Silver;
            }
            button2 = button;
        }
        #endregion

        #region Ask12
        Button button12 = null;
        bool isAsk12 = false;
        private void Button_Clicked_7(object sender, EventArgs e)
        {
            isAsk12 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askPageMV.Ask.Safe_delivery_location = button.Text;
            if (button12 != null)
            {
                button12.TextColor = Color.Silver;
            }
            button12 = button;
        }
        #endregion

        #region Ask15
        bool isAsk15 = false;
        Button button15 = null;
        private void Button_Clicked_8(object sender, EventArgs e)
        {
            isAsk15 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askPageMV.Ask.Enough_space_to_take_pictures = button.Text;
            if (button15 != null)
            {
                button15.TextColor = Color.Silver;
            }
            button15 = button;
        }
        #endregion

        #region Ask4
        bool isAsk4 = false;
        private void RadioButton_Clicked(object sender, EventArgs e)
        {
            askPageMV.Ask.Weather_conditions = ((RadioButton)sender).Text;
            isAsk4 = true;
        }
        #endregion

        #region Ask7
        bool isAsk7 = false;
        string txtAnswer7 = "";
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            isAsk7 = true;
            txtAnswer7 = e.NewTextValue;
            askPageMV.Ask.Anyone_Rushing_you_to_perform_the_inspection = $"{btnAnswer7} {txtAnswer7}";
        }

        Button button7 = null;
        string btnAnswer7 = "";
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            isAsk1 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            btnAnswer7 = button.Text;
            askPageMV.Ask.Anyone_Rushing_you_to_perform_the_inspection = $"{btnAnswer7} {txtAnswer7}";
            if (button1 != null)
            {
                button1.TextColor = Color.Silver;
            }
            button1 = button;
            if(button.Text == "Yes" && button.Text == "YES")
            {
                askBlock7v2.IsVisible = true;
                if (txtAnswer7 != "")
                {
                    isAsk7 = true;
                }
                else
                {
                    isAsk7 = false;
                }
            }
            else
            {
                askBlock7v2.IsVisible = false;
                isAsk7 = true;
            }
        }
        #endregion

        #region Ask9
        bool isAsk9 = false;
        private void Entry_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk9 = true;
            }
            else
            {
                isAsk9 = false;
            }
            askPageMV.Ask.Plate = e.NewTextValue;
        }
        #endregion

        #region Ask13
        bool isAsk13 = false;
        private void Entry_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk13 = true;
            }
            else
            {
                isAsk13 = false;
            }
            askPageMV.Ask.How_far_from_trailer = e.NewTextValue;
        }
        #endregion

        #region Ask14
        bool isAsk14 = false;
        private void Entry_TextChanged_5(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk14 = true;
            }
            else
            {
                isAsk14 = false;
            }
            askPageMV.Ask.Name_of_the_person_who_gave_you_keys = e.NewTextValue;
        }
        #endregion

        #region Ask11
        bool isAsk11 = false;
        private void Dropdown_SelectedItemChanged_1(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk11 = true;
            askPageMV.Ask.TypeVehicle = (string)e.NewItem;
            askPageMV.VehiclwInformation.Type = askPageMV.Ask.TypeVehicle;
        }
        #endregion

        [Obsolete]
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk2 && isAsk12 && isAsk4 && isAsk13 && isAsk14 && isAsk7 && isAsk14 && isAsk9 && isAsk10 && isAsk11)
            {
                askPageMV.SaveAsk(askPageMV.Ask.TypeVehicle.Replace(" ", ""));
            }
            else
            {
                await PopupNavigation.PushAsync(new Errror("You did not fill in all the required fields, you can continue the inspection only when filling in the required fields !!", null));
                CheckAsk();
            }
        }

        private void CheckAsk()
        {
            if (!isAsk1)
            {
                askBlock1.BorderColor = Color.Red;
            }
            else
            {
                askBlock1.BorderColor = Color.BlueViolet;
            }
            if (!isAsk2)
            {
                askBlock2.BorderColor = Color.Red;
            }
            else
            {
                askBlock2.BorderColor = Color.BlueViolet;
            }
            if (!isAsk12)
            {
                askBlock12.BorderColor = Color.Red;
            }
            else
            {
                askBlock12.BorderColor = Color.BlueViolet;
            }
            if (!isAsk4)
            {
                askBlock4.BorderColor = Color.Red;
            }
            else
            {
                askBlock4.BorderColor = Color.BlueViolet;
            }
            if (!isAsk13)
            {
                askBlock13.BorderColor = Color.Red;
            }
            else
            {
                askBlock13.BorderColor = Color.BlueViolet;
            }
            if (!isAsk14)
            {
                askBlock14.BorderColor = Color.Red;
            }
            else
            {
                askBlock14.BorderColor = Color.BlueViolet;
            }
            if (!isAsk7)
            {
                askBlock7.BorderColor = Color.Red;
            }
            else
            {
                askBlock7.BorderColor = Color.BlueViolet;
            }
            if (!isAsk15)
            {
                askBlock15.BorderColor = Color.Red;
            }
            else
            {
                askBlock15.BorderColor = Color.BlueViolet;
            }
            if (!isAsk9)
            {
                askBlock9.BorderColor = Color.Red;
            }
            else
            {
                askBlock9.BorderColor = Color.BlueViolet;
            }
            if (!isAsk10)
            {
                askBlock10.BorderColor = Color.Red;
            }
            else
            {
                askBlock10.BorderColor = Color.BlueViolet;
            }
            if (!isAsk11)
            {
                askBlock11.BorderColor = Color.Red;
            }
            else
            {
                askBlock11.BorderColor = Color.BlueViolet;
            }
        }

        #region Ask10
        bool isAsk10 = false;
        Button button10 = null;
        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            isAsk10 = true;
            await Navigation.PushAsync(new CameraItems(this));
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            if (button10 != null)
            {
                button10.TextColor = Color.Silver;
            }
            button10 = button;
        }

        private async void Button_Clicked_6v1(object sender, EventArgs e)
        {
            isAsk10 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            if (button10 != null)
            {
                button1.TextColor = Color.Silver;
            }
            button10 = button;
        }
        #endregion

        public void AddPhotoItems(byte[] photob)
        {
            if (askPageMV.Ask.Any_personal_or_additional_items_with_or_in_vehicle == null)
            {
                askPageMV.Ask.Any_personal_or_additional_items_with_or_in_vehicle = new List<Models.Photo>();
            }
            Models.Photo photo = new Models.Photo();
            photo.Base64 = JsonConvert.SerializeObject(photob);
            photo.path = $"../Photo/{askPageMV.VehiclwInformation.Id}/PikedUp/Items/{askPageMV.Ask.Any_personal_or_additional_items_with_or_in_vehicle.Count + 1}.jpg";
            askPageMV.Ask.Any_personal_or_additional_items_with_or_in_vehicle.Add(photo);
            Image image = new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photob)),
                HeightRequest = 50,
                WidthRequest = 50,
            };
            image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke1));
            blockAskPhotoItem.Children.Add(image);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async void ViewPhotoForRetacke1(Xamarin.Forms.View v, object s)
        {
            if (v != null && blockAskPhotoItem.Children.Contains(v))
            {
                await Navigation.PushAsync(new ViewPhotForAsk(v, this, "Ask1"));
            }
        }

        public void ReSetPhoto2(Xamarin.Forms.View view, byte[] newRetake)
        {
            byte[] r = GetImageBytes(((Image)view).Source);
            askPageMV.ResetAskPhotoItem(r, newRetake);
            blockAskPhotoItem.Children.Remove((Image)view);
            Image image = new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(newRetake)),
                HeightRequest = 50,
                WidthRequest = 50,
            };
            image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke1));
            blockAskPhotoItem.Children.Add(image);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private byte[] GetImageBytes(ImageSource imagesource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imagesource;
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                var stream = streamImageSource.Stream.Invoke(new System.Threading.CancellationToken()).Result;
                stream.CopyTo(memoryStream);
                stream = null;
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAsk11 = true;
            askPageMV.Ask.TypeVehicle = (string)((Picker)sender).SelectedItem;
            askPageMV.VehiclwInformation.Type = askPageMV.Ask.TypeVehicle;
        }
    }
}