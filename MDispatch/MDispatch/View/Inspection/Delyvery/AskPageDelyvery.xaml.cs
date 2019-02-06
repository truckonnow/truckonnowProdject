using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using Plugin.InputKit.Shared.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.Delyvery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AskPageDelyvery : ContentPage
	{
        AskDelyveryMV askDelyveryMV = null;

        public AskPageDelyvery (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, string idShip, InitDasbordDelegate initDasbordDelegate, GetVechicleDelegate getVechicleDelegate)
		{
            askDelyveryMV = new AskDelyveryMV(managerDispatchMob, vehiclwInformation, idShip, Navigation, initDasbordDelegate, getVechicleDelegate);
            askDelyveryMV.AskDelyvery = new AskDelyvery();
            InitializeComponent ();
            BindingContext = askDelyveryMV;
            InitAsk();
        }

        #region Ask1
        string curentTime = null;
        string currentDate = null;

        private void InitAsk()
        {
            curentTime = askDelyveryMV.TimeOfCurren.ToString("hh':'mm");
            currentDate = DateTime.Now.ToShortDateString();
            askDelyveryMV.AskDelyvery.Time_Of_Delivery = $"{currentDate} {curentTime}";
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            currentDate = e.NewDate.ToShortDateString();
            askDelyveryMV.AskDelyvery.Time_Of_Delivery = $"{currentDate} {curentTime}";
        }

        private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                curentTime = time.Time.ToString("hh':'mm");
                askDelyveryMV.AskDelyvery.Time_Of_Delivery = $"{currentDate} {curentTime}";
            }
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
            askDelyveryMV.AskDelyvery.Lightbrightness = button.Text;
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
            askDelyveryMV.AskDelyvery.Vehicle_Condition_on_delivery = button.Text;
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
            askDelyveryMV.AskDelyvery.How_did_you_get_inside_of_the_vehicle = ((RadioButton)sender).Text;
            isAsk4 = true;
        }
        #endregion

        #region Ask5
        bool isAsk5 = false;
        private void Dropdown_SelectedItemChanged_1(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk5 = true;
            askDelyveryMV.AskDelyvery.How_did_you_get_inside_of_the_vehicle = (string)e.NewItem;
        }
        #endregion

        #region Ask6
        Button button6 = null;
        bool isAsk6 = false;
        private void Button_Clicked_6(object sender, EventArgs e)
        {
            isAsk6 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askDelyveryMV.AskDelyvery.Did_the_vehicle_starts = button.Text;
            if (button6 != null)
            {
                button6.TextColor = Color.Silver;
            }
            button6 = button;
        }
        #endregion

        #region Ask7
        Button button7 = null;
        bool isAsk7 = false;
        private void Button_Clicked_7(object sender, EventArgs e)
        {
            isAsk7 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askDelyveryMV.AskDelyvery.Does_the_vehicle_Drives = button.Text;
            if (button7 != null)
            {
                button7.TextColor = Color.Silver;
            }
            button7 = button;
        }
        #endregion

        #region Ask8
        bool isAsk8 = false;
        private void Entry_TextChanged_8(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk8 = true;
            }
            else
            {
                isAsk8 = false;
            }
            askDelyveryMV.AskDelyvery.Anyone_Rushing_you_to_perform_the_delivery = e.NewTextValue;
        }
        #endregion

        #region Ask9
        bool isAsk9 = false;
        private void Entry_TextChanged_9(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk9 = true;
            }
            else
            {
                isAsk9 = false;
            }
            askDelyveryMV.AskDelyvery.How_Far_is_the_Trailer_from_Delivery_destination = e.NewTextValue;
        }
        #endregion

        #region Ask10
        bool isAsk10 = false;
        private void Entry_TextChanged_10(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk10 = true;
            }
            else
            {
                isAsk10 = false;
            }
            askDelyveryMV.AskDelyvery.Exact_mileage_after_unloading = e.NewTextValue;
        }
        #endregion

        #region Ask11
        bool isAsk11 = false;
        private void Entry_TextChanged_11(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk11 = true;
            }
            else
            {
                isAsk11 = false;
            }
            askDelyveryMV.AskDelyvery.Anyone_helping_you_unload = e.NewTextValue;
        }
        #endregion

        #region Ask12
        bool isAsk12 = false;
        private void Entry_TextChanged_12(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk12 = true;
            }
            else
            {
                isAsk12 = false;
            }
            askDelyveryMV.AskDelyvery.Did_someone_else_unloaded_the_vehicle_for_you = e.NewTextValue;
        }
        #endregion

        #region Ask13
        bool isAsk13 = false;
        private void Entry_TextChanged_13(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk13 = true;
            }
            else
            {
                isAsk13 = false;
            }
            askDelyveryMV.AskDelyvery.Did_you_notice_any_imperfections_on_body_wile_vehicle_been_transported = e.NewTextValue;
        }
        #endregion

        #region Ask14
        bool isAsk14 = false;
        private void Entry_TextChanged_14(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk14 = true;
            }
            else
            {
                isAsk14 = false;
            }
            askDelyveryMV.AskDelyvery.How_many_keys_are_you_giving_to_client = e.NewTextValue;
        }
        #endregion

        #region Ask15
        bool isAsk15 = false;
        private void Entry_TextChanged_15(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk15 = true;
            }
            else
            {
                isAsk15 = false;
            }
            askDelyveryMV.AskDelyvery.Are_you_giving_any_paperwork_to_a_client = e.NewTextValue;
        }
        #endregion

        #region Ask16
        Button button16 = null;
        bool isAsk16 = false;
        private void Button_Clicked_16(object sender, EventArgs e)
        {
            isAsk16 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askDelyveryMV.AskDelyvery.How_did_you_get_inside_of_the_vehicle = button.Text;
            if (button16 != null)
            {
                button16.TextColor = Color.Silver;
            }
            button16 = button;
        }
        #endregion


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk2 && isAsk3 && isAsk4 && isAsk5 && isAsk6 && isAsk7 && isAsk8 && isAsk9 && isAsk10 && isAsk11 && isAsk12 && isAsk13 && isAsk14 && isAsk15 && isAsk16)
            {
                askDelyveryMV.SaveAsk();
            }
            else
            {
                CheckAsk();
            }
        }

        private void CheckAsk()
        {
            if (!isAsk2)
            {
                askBlock2.BorderColor = Color.Red;
            }
            else
            {
                askBlock2.BorderColor = Color.BlueViolet;
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
            if (!isAsk14)
            {
                askBlock14.BorderColor = Color.Red;
            }
            else
            {
                askBlock14.BorderColor = Color.BlueViolet;
            }
            if (!isAsk15)
            {
                askBlock15.BorderColor = Color.Red;
            }
            else
            {
                askBlock15.BorderColor = Color.BlueViolet;
            }
            if (!isAsk16)
            {
                askBlock16.BorderColor = Color.Red;
            }
            else
            {
                askBlock16.BorderColor = Color.BlueViolet;
            }
        }
    }
}