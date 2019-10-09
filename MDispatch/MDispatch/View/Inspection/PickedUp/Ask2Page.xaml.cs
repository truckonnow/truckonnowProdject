using MDispatch.Models;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Plugin.InputKit.Shared.Controls;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.PickedUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ask2Page : ContentPage
    {
        private Ask2PageMW ask2PageMW = null;

        public Ask2Page(ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate)
        {
            ask2PageMW = new Ask2PageMW(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate);
            ask2PageMW.Ask2 = new Ask2();
            InitializeComponent();
            BindingContext = ask2PageMW;
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
            ask2PageMW.Ask2.How_many_keys_total_you_been_given = e.NewTextValue;
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
            ask2PageMW.Ask2.Any_additional_documentation_been_given_after_loading = e.NewTextValue;
        }
        #endregion

        #region Ask3
        bool isAsk3 = false;
        private void Entry_TextChanged2(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != "")
            {
                isAsk3 = true;
            }
            else
            {
                isAsk3 = false;
            }
            ask2PageMW.Ask2.Any_additional_parts_been_given_to_you = e.NewTextValue;
        }
        #endregion

        #region Ask4
        Button button4 = null;
        bool isAsk4 = false;
        private void Button_Clicked4(object sender, EventArgs e)
        {
            isAsk4 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            ask2PageMW.Ask2.Car_locked = button.Text;
            if (button4 != null)
            {
                button4.TextColor = Color.Silver;
            }
            button4 = button;
        }
        #endregion

        #region Ask5
        bool isAsk5 = false;
        Button button5= null;
        private void Button_Clicked(object sender, EventArgs e)
        {
            isAsk5 = true;
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            ask2PageMW.Ask2.Car_locked = button.Text;
            if (button5 != null)
            {
                button5.TextColor = Color.Silver;
            }
            button5 = button;
        }
        #endregion

        #region Ask6
        bool isAsk6 = false;
        private void AdvancedSlider_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                isAsk6 = true;
                ask2PageMW.Ask2.Client_friendliness = ((AdvancedSlider)sender).Value.ToString();
            }
        }
        #endregion  


        [Obsolete]
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (isAsk1 && isAsk3 && isAsk4 && isAsk5 && isAsk6)
            {
                ask2PageMW.SaveAsk();
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
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BOLPage(ask2PageMW.managerDispatchMob, ask2PageMW.IdShip, ask2PageMW.initDasbordDelegate));
        }
    }
}