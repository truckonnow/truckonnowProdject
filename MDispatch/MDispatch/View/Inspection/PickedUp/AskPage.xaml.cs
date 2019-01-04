using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.AskPhoto.CameraPageFolder;
using MDispatch.ViewModels.AskPhoto;
using Newtonsoft.Json;
using Plugin.InputKit.Shared.Controls;
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
        private Models.Ask Ask = null;

        public AskPage (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping, InitDasbordDelegate initDasbordDelegate)
		{
            askPageMV = new AskPageMV(managerDispatchMob, vehiclwInformation, shipping, Navigation, initDasbordDelegate);
            Ask = new Models.Ask();
            InitializeComponent ();
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
            Ask.Lightbrightness = button.Text;
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
            Ask.Vehicle = button.Text;
            if (button2 != null)
            {
                button2.TextColor = Color.Silver;
            }
            button2 = button;
        }
        #endregion

        #region Ask3
        Button button3 = null;
        bool isAsk3 = false;
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            isAsk3 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask.Enough_distance_to_take_pictures_at_least_4ft = button.Text;
            if (button3 != null)
            {
                button3.TextColor = Color.Silver;
            }
            button3 = button;
        }
        #endregion

        #region Ask4
        bool isAsk4 = false;
        private void RadioButton_Clicked(object sender, EventArgs e)
        {
            Ask.Weather_conditions = ((RadioButton)sender).Text;
            isAsk4 = true;
        }
        #endregion

        #region Ask5
        Button button5 = null;
        bool isAsk5 = false;
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            isAsk5 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask.Does_The_vehicle_Starts = button.Text;
            if (button5 != null)
            {
                button5.TextColor = Color.Silver;
            }
            button5 = button;
        }
        #endregion

        #region Ask6
        Button button6 = null;
        bool isAsk6 = false;
        private void Button_Clicked_4(object sender, EventArgs e)
        {
            isAsk6 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            Ask.Does_The_vehicle_Drives = button.Text;
            if (button6 != null)
            {
                button6.TextColor = Color.Silver;
            }
            button6 = button;
        }
        #endregion

        #region Ask7
        bool isAsk7 = false;
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue != "")
            {
                isAsk7 = true;
            }
            else
            {
                isAsk7 = false;
            }
            Ask.Anyone_Rushing_you_to_perform_the_inspection = e.NewTextValue;
        }
        #endregion

        #region Ask8
        bool isAsk8 = false;
        private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk8 = true;
            }
            else
            {
                isAsk8 = false;
            }
            Ask.How_far_is_the_vehicle_from_Trailer_Aprox_in_ft = e.NewTextValue;
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
            Ask.Plate = e.NewTextValue;
        }
        #endregion

        #region Ask10
        bool isAsk10 = false;
        private void Entry_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk10 = true;
            }
            else
            {
                isAsk10 = false;
            }
            Ask.Exact_Mileage = e.NewTextValue;
        }
        #endregion

        #region Ask11
        bool isAsk11 = false;
        private void Dropdown_SelectedItemChanged_1(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk11 = true;
            Ask.TypeVehicle = (string)e.NewItem;
        }
        #endregion

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if(isAsk1 && isAsk2 && isAsk3 && isAsk4 && isAsk5 && isAsk6 && isAsk7 && isAsk8 && isAsk9 && isAsk10 && isAsk11)
            {
                askPageMV.Ask = Ask;
                askPageMV.SaveAsk(Ask.TypeVehicle.Replace(" ", ""));
            }
            else
            {
                CheckAsk();
            }
        }

        private void CheckAsk()
        {
            if(!isAsk1)
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
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraDocument(this));
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CameraItems(this));
        }

        public void AddPhotoDocumentom(byte[] photob)
        {
            if(Ask.Any_paperwork_or_documentation == null)
            {
                Ask.Any_paperwork_or_documentation = new List<Models.Photo>();
            }
            Models.Photo photo = new Models.Photo();
            photo.Base64 = JsonConvert.SerializeObject(photob);
            photo.path = $"Photo/PikedUp/{askPageMV.VehiclwInformation.Id}/Ask/Document/{ Ask.Any_paperwork_or_documentation.Count + 1}.png";
            Ask.Any_paperwork_or_documentation.Add(photo);
            blockAskPhotoDocument.Children.Add(new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photob)),
                HeightRequest = 50,
                WidthRequest = 50
            });
        }

        public void AddPhotoItems(byte[] photob)
        {
            if (Ask.Any_personal_or_additional_items_with_or_in_vehicle == null)
            {
                Ask.Any_personal_or_additional_items_with_or_in_vehicle = new List<Models.Photo>();
            }
            Models.Photo photo = new Models.Photo();
            photo.Base64 = JsonConvert.SerializeObject(photob);
            photo.path = $"Photo/PikedUp/{askPageMV.VehiclwInformation.Id}/Ask/Items/{ Ask.Any_personal_or_additional_items_with_or_in_vehicle.Count + 1}.png";
            Ask.Any_personal_or_additional_items_with_or_in_vehicle.Add(photo);
            blockAskPhotoItem.Children.Add(new Image()
            {
                Source = ImageSource.FromStream(() => new MemoryStream(photob)),
                HeightRequest = 50,
                WidthRequest = 50
            });
        }
    }
}