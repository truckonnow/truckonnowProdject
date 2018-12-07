using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.AskPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.AskPhoto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AskPage : ContentPage
	{
        AskPageMV askPageMV = null;

        public AskPage (ManagerDispatchMob managerDispatchMob)
		{
            askPageMV = new AskPageMV(managerDispatchMob);
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
            button.TextColor = Color.FromHex("#65CAE1");
            if(button1 != null)
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
            button.TextColor = Color.FromHex("#65CAE1");
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
            button.TextColor = Color.FromHex("#65CAE1");
            if (button3 != null)
            {
                button3.TextColor = Color.Silver;
            }
            button3 = button;
        }
        #endregion

        #region Ask4
        bool isAsk4 = false;
        private void RadioButtonGroupView_SelectedItemChanged(object sender, EventArgs e)
        {
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
            button.TextColor = Color.FromHex("#65CAE1");
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
            button.TextColor = Color.FromHex("#65CAE1");
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
        }
        #endregion

        #region Ask9
        bool isAsk9 = false;
        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk9 = true;
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
        }
        #endregion

        #region Ask11
        bool isAsk11 = false;
        private void Dropdown_SelectedItemChanged_1(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            isAsk11 = true;
        }
        #endregion
    }
}