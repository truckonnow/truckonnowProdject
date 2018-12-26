using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AskForUser : ContentPage
	{
        private AskForUserMV askForUserMV = null;
        private AskForUserM askForUserM = null;

        public AskForUser (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shippin)
		{
            askForUserMV = new AskForUserMV(managerDispatchMob, vehiclwInformation, shippin, Navigation);
            askForUserM = new AskForUserM();
            InitializeComponent ();
            BindingContext = askForUserM;
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
            askForUserM.Your_Full_Name = e.NewTextValue;
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
            askForUserM.Your_phone = e.NewTextValue;
        }
        #endregion

        #region Ask3
        bool isAsk3 = false;
        private void Entry_TextChanged3(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk3 = true;
            }
            else
            {
                isAsk3 = false;
            }
            askForUserM.How_many_keys_are_driver_been_given = e.NewTextValue;
        }

        #endregion

        #region Ask4
        Button button4 = null;
        bool isAsk4 = false;
        private void Button_Clicked(object sender, EventArgs e)
        {
            isAsk4 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            askForUserM.Any_titles_been_given_to_driver = button.Text;
            if (button4 != null)
            {
                button4.TextColor = Color.Silver;
            }
            button4 = button;
        }
        #endregion

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk2 && isAsk3 && isAsk4)
            {
                askForUserMV.AskForUser = askForUserM;
                askForUserMV.SaveAsk();
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
        }
    }
}