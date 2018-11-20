using MDispatch.Service;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.TAbbMV;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.TabPage.Tab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivePage : ContentPage
	{
        public ActiveMV activeMV = null;
        private StackLayout SelectStackLayout = null;

        public ActivePage (ManagerDispatchMob managerDispatchMob, INavigation navigation)
		{
            this.activeMV = new ActiveMV(managerDispatchMob, navigation);
			InitializeComponent ();
            BindingContext = this.activeMV;
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((StackLayout)sender).FindByName<StackLayout>("st");
            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
            }
            SelectStackLayout = stackLayout;
            SelectStackLayout.BackgroundColor = Color.FromHex("#f5c8c8");
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
                SelectStackLayout = null;
            }
            string idOrder = null;
            StackLayout stackLayout = (StackLayout)sender;
            Label idorderL = stackLayout.FindByName<Label>("idOrder");
            if(idorderL != null)
            {
                idOrder = idorderL.Text;
            }
            else
            {
                idOrder = stackLayout.Parent.Parent.FindByName<Label>("idOrder").Text;
            }
            await activeMV.Navigation.PushAsync(new InfoOrder(activeMV.managerDispatchMob, activeMV.Shippings.Find(s => s.Id == idOrder)));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped_1(sender, e);
        }
    }
}