using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
using MDispatch.View.Inspection.PickedUp.CameraPageFolder;
using MDispatch.View.Inspection.PickUp.CameraPageFolder;
using MDispatch.ViewModels.InspectionMV;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
        private string typeCar = null;

        public Ask1Page(ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate, string typeCar,
            string onDeliveryToCarrier, string totalPaymentToCarrier)
        {
            this.typeCar = typeCar;
            ask1PageMV = new Ask1PageMV(managerDispatchMob, vehiclwInformation, idShip, Navigation, initDasbordDelegate, getVechicleDelegate, onDeliveryToCarrier, totalPaymentToCarrier);
            ask1PageMV.Ask1 = new Ask1();
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
            ask1PageMV.Ask1.Exact_Mileage = e.NewTextValue;
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
            ask1PageMV.Ask1.Did_someone_help_you_load_it = button.Text;
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
            ask1PageMV.Ask1.Did_someone_load_the_vehicle_for_you = e.NewTextValue;
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
            ask1PageMV.Ask1.Did_someone_load_the_vehicle_for_you = e.NewTextValue;
        }
        #endregion

        #region Ask6
        bool isAsk6 = false;
        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk6 = true;
            ask1PageMV.Ask1.What_method_of_exit_did_you_use = (string)e.NewItem;
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
            ask1PageMV.Ask1.Did_you_jumped_the_vehicle_to_start = button.Text;
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
            ask1PageMV.Ask1.Have_you_used_winch = button.Text;
            if (button2 != null)
            {
                button2.TextColor = Color.Silver;
            }
            button2 = button;
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
                ask1PageMV.Ask1.App_will_force_driver_to_take_pictures_of_each_strap = new List<Photo>(photos);
                foreach (var imageByte in imagesByte)
                {
                    Image image = new Image()
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(imageByte)),
                        HeightRequest = 50,
                        WidthRequest = 50

                    };
                    image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke2));
                    blockAskPhotoSeatBelts.Children.Add(image);
                }
            }
            else
            {
                isAsk12 = false;
            }
        }

        private async void ViewPhotoForRetacke2(Xamarin.Forms.View v, object s)
        {
            if (v != null && blockAskPhotoSeatBelts.Children.Contains(v))
            {
                await Navigation.PushAsync(new ViewPhotForAsk(v, this, "Ask5"));
            }
        }

        public void ReSetPhoto2(Xamarin.Forms.View view, byte[] newRetake)
        {
            byte[] r = GetImageBytes(((Image)view).Source);
            ask1PageMV.ResetAskSeatBelts(r, newRetake);
            blockAskPhotoSeatBelts.Children.Remove((Image)view);
            Image image = new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(newRetake)),
                HeightRequest = 50,
                WidthRequest = 50,
            };
            image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke2));
            blockAskPhotoSeatBelts.Children.Add(image);
        }
        #endregion

        [Obsolete]
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk14 && isAsk3 && isAsk4 && isAsk5 && isAsk6 && isAsk7 && isAsk8  && isAsk12 && isAsk13)
            {
                ask1PageMV.SaveAsk();
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
            if (!isAsk14)
            {
                askBlock14.BorderColor = Color.Red;
            }
            else
            {
                askBlock14.BorderColor = Color.BlueViolet;
            }
            if (!isAsk3)
            {
                askBlock3.BorderColor = Color.Red;
            }
            else
            {
                askBlock3.BorderColor = Color.BlueViolet;
            }
            if (!isAsk4)
            {
                askBlock4.BorderColor = Color.Red;
            }
            else
            {
                askBlock4.BorderColor = Color.BlueViolet;
            }
            if (!isAsk5)
            {
                askBlock5.BorderColor = Color.Red;
            }
            else
            {
                askBlock5.BorderColor = Color.BlueViolet;
            }
            if (!isAsk6)
            {
                askBlock6.BorderColor = Color.Red;
            }
            else
            {
                askBlock6.BorderColor = Color.BlueViolet;
            }
            if (!isAsk7)
            {
                askBlock7.BorderColor = Color.Red;
            }
            else
            {
                askBlock7.BorderColor = Color.BlueViolet;
            }
            if (!isAsk8)
            {
                askBlock8.BorderColor = Color.Red;
            }
            else
            {
                askBlock8.BorderColor = Color.BlueViolet;
            }
            if (!isAsk12)
            {
                askBlock12.BorderColor = Color.Red;
            }
            else
            {
                askBlock12.BorderColor = Color.BlueViolet;
            }
            if (!isAsk13)
            {
                askBlock13.BorderColor = Color.Red;
            }
            else
            {
                askBlock13.BorderColor = Color.BlueViolet;
            }
        }
        
        #region Ask13
        bool isAsk13 = false;
        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraPthotoInTrack(this, typeCar.Replace(" ", "")));
        }

        public void AddPhotoInTrack(List<Photo> photos, List<byte[]> imagesByte)
        {
            if (photos != null)
            {
                isAsk13 = true;
                ask1PageMV.Ask1.Photo_after_loading_in_the_truck = new List<Photo>(photos);
                foreach (var imageByte in imagesByte)
                {
                    Image image = new Image()
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(imageByte)),
                        HeightRequest = 50,
                        WidthRequest = 50
                    };
                    image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke3));
                    blockPhotoInTrack.Children.Add(image);
                }
            }
            else
            {
                isAsk13 = false;
            }
        }

        private async void ViewPhotoForRetacke3(Xamarin.Forms.View v, object s)
        {
            if (v != null && blockPhotoInTrack.Children.Contains(v))
            {
                await Navigation.PushAsync(new ViewPhotForAsk(v, this, "Ask6"));
            }
        }

        public void ReSetPhoto3(Xamarin.Forms.View view, byte[] newRetake)
        {
            byte[] r = GetImageBytes(((Image)view).Source);
            ask1PageMV.ResetAskInTrack(r, newRetake);
            blockPhotoInTrack.Children.Remove((Image)view);
            Image image = new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(newRetake)),
                HeightRequest = 50,
                WidthRequest = 50,
            };
            image.GestureRecognizers.Add(new TapGestureRecognizer(ViewPhotoForRetacke3));
            blockPhotoInTrack.Children.Add(image);
        }
        #endregion

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
            isAsk6 = true;
            ask1PageMV.Ask1.What_method_of_exit_did_you_use = (string)((Picker)sender).SelectedItem;
        }

        #region Ask14
        Button button14 = null;
        bool isAsk14 = false;
        bool isTypeText = false;
        string btnText = "";
        string entryText = "";
        private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk14 = true;
                isTypeText = true;
            }
            else
            {
                isAsk14 = false;
                isTypeText = false;
            }
            entryText = e.NewTextValue;
            ask1PageMV.Ask1.Did_you_notice_any_mechanical_imperfections_wile_loading = $"{btnText}, {entryText}";
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            btnText = button.Text;
            if (button14 != null)
            {
                button14.TextColor = Color.Silver;
            }
            if(button.Text == "Yes")
            {
                if(isTypeText)
                {
                    isAsk14 = true;
                }
                else
                {
                    isAsk14 = false;
                }
                nameE.IsVisible = true;
            }
            else
            {
                isAsk14 = true;
                nameE.IsVisible = false;
            }
            button14 = button;

            ask1PageMV.Ask1.Did_you_notice_any_mechanical_imperfections_wile_loading = $"{btnText}, {entryText}";
        }
        #endregion
    }
}