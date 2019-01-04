using MDispatch.Models;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV;
using Plugin.InputKit.Shared.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Feedback : ContentPage
	{
        FeedBackMV feedBackMV = null;
        Models.Feedback feedback = null;

        public Feedback (ManagerDispatchMob managerDispatchMob, VehiclwInformation vehiclwInformation, Shipping shipping)
		{
            feedBackMV = new FeedBackMV(managerDispatchMob, vehiclwInformation, shipping , Navigation);
			InitializeComponent ();
            BindingContext = feedBackMV;
            Init();
		}

        private void Init()
        {
            feedback = new Models.Feedback();
            feedback.How_Are_You_Satisfied_With_Service = "0";
            feedback.Would_You_Use_Our_Company_Again = "No";
            feedback.Would_You_Like_To_Get_An_notification_If_We_Have_Any_Promotion = "No";
            feedback.How_did_the_driver_perform = "0";
        }

        private void AdvancedSlider_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "value")
            {
                feedback.How_Are_You_Satisfied_With_Service = ((AdvancedSlider)sender).Value.ToString();
            }
        }
        
        Button button4 = null;
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            feedback.Would_You_Use_Our_Company_Again = button.Text;
            if (button4 != null)
            {
                button4.TextColor = Color.Silver;
            }
            button4 = button;
        }

        Button button1 = null;
        private void Button_Clicked1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.TextColor = Color.FromHex("#4fd2c2");
            feedback.Would_You_Like_To_Get_An_notification_If_We_Have_Any_Promotion = button.Text;
            if (button1 != null)
            {
                button1.TextColor = Color.Silver;
            }
            button1 = button;
        }

        private void AdvancedSlider_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "value")
            {
                feedback.How_did_the_driver_perform = ((AdvancedSlider)sender).Value.ToString();
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            feedBackMV.Feedback = feedback;
            feedBackMV.SaveAsk();
        }
    }
}