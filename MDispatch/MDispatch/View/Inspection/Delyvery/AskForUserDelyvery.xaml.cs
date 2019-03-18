using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.Inspection.PickedUp;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.Delyvery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AskForUserDelyvery : ContentPage
	{
        private AskForUsersDelyveryMW askForUsersDelyveryMW = null;


        public AskForUserDelyvery (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate)
		{
            askForUsersDelyveryMW = new AskForUsersDelyveryMW(managerDispatchMob, vehiclwInformation, idShip, Navigation, initDasbordDelegate);
            askForUsersDelyveryMW.AskForUserDelyveryM = new AskForUserDelyveryM();
            InitializeComponent ();
            BindingContext = askForUsersDelyveryMW;
		}

        #region Ask1
        Button button1 = null;
        bool isAsk1 = false;
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            isAsk1 = true;
            if (button1 != null)
            {
                button1.TextColor = Color.Silver;
            }
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up = button.Text;
            button1 = button;
            if (button1.Text == "No, found an issue")
            {
                await Navigation.PushAsync(new PageAddDamageFoUser(askForUsersDelyveryMW, blockAskPhoto, this));
            }
            else if(button1.Text == "Yes")
            {
                askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo = null;
                blockAskPhoto.Children.Clear();
                scrolViewAskPhoto.IsVisible = false;
            }
        }

        public void AddPhotoAdditional(byte[] image)
        {
            if(askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo == null)
            {
                askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo = new List<Photo>();
            }
            Photo photo1 = new Photo();
            photo1.Base64 = JsonConvert.SerializeObject(image);
            photo1.path = $"../Photo/{askForUsersDelyveryMW.VehiclwInformation.Id}/Delyvery/Additional/{askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo.Count + 1}.Jpeg";
            askForUsersDelyveryMW.AskForUserDelyveryM.Have_you_inspected_the_vehicle_For_any_additional_imperfections_other_than_listed_at_the_pick_up_photo.Add(photo1);
            blockAskPhoto.Children.Add(new Image() { Source = ImageSource.FromStream(() => new MemoryStream(image)), HeightRequest = 40, WidthRequest = 40 });
            if (!scrolViewAskPhoto.IsVisible)
            {
                scrolViewAskPhoto.IsVisible = true;
            }
        }
        #endregion

        #region Ask2
        bool isAsk2 = false;
        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk2 = true;
            askForUsersDelyveryMW.AskForUserDelyveryM.What_form_of_payment_are_you_using_to_pay_for_transportation = (string)e.NewItem;
        }
        #endregion

        #region Ask3
        bool isSignatureAsk = false;
        bool isNameAsk = false;
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isNameAsk = true;
            }
            else
            {
                isNameAsk = false;
            }
            askForUsersDelyveryMW.AskForUserDelyveryM.App_will_ask_for_name_of_the_client_signature = e.NewTextValue;
        }

        private async void Sign_StrokeCompleted(object sender, EventArgs e)
        {
            Photo photo = new Photo();
            isSignatureAsk = true;
            Stream stream = await sign.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            byte[] image = memoryStream.ToArray();
            photo.Base64 = JsonConvert.SerializeObject(image);
            photo.path = $"../Photo/{askForUsersDelyveryMW.VehiclwInformation.Id}/Delyvery/Signature/DelyverySig.Png";
            askForUsersDelyveryMW.AskForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature = photo;
        }

        private void Sign_Cleared(object sender, EventArgs e)
        {
            isSignatureAsk = false;
            askForUsersDelyveryMW.AskForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature = null;
        }

        private bool GetIsAsk3()
        {
            return isNameAsk == true && isSignatureAsk == true;
        }
        #endregion

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk2 && GetIsAsk3())
            {
                askForUsersDelyveryMW.SaveAsk();
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
                askBlock2.BorderColor = Color.Red;
            }
            else
            {
                askBlock2.BorderColor = Color.BlueViolet;
            }
            if (!isAsk2)
            {
                askBlock2.BorderColor = Color.Red;
            }
            else
            {
                askBlock2.BorderColor = Color.BlueViolet;
            }
            if (!GetIsAsk3())
            {
                askBlock3.BorderColor = Color.Red;
            }
            else
            {
                askBlock3.BorderColor = Color.BlueViolet;
            }
        }
    }
}