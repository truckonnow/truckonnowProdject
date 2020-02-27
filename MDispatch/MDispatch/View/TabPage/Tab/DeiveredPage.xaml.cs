using MDispatch.Service;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.TAbbMV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.TabPage.Tab
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeiveredPage : ContentPage
	{
        public DelyveryMV delyveryMV = null;
        private StackLayout SelectStackLayout = null;
        private InitDasbordDelegate initDasbordDelegate = null;

        public DeiveredPage(ManagerDispatchMob managerDispatchMob, INavigation navigation)
        {
            this.initDasbordDelegate = initDasbordDelegate;
            this.delyveryMV = new DelyveryMV(managerDispatchMob, navigation);
            InitializeComponent();
            BindingContext = this.delyveryMV;
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
            if (idorderL != null)
            {
                idOrder = idorderL.Text;
            }
            else
            {
                idOrder = stackLayout.Parent.Parent.FindByName<Label>("idOrder").Text;
            }
            await delyveryMV.Navigation.PushAsync(new InfoOrder(delyveryMV.managerDispatchMob, delyveryMV.Shippings.Find(s => s.Id == idOrder), delyveryMV.initDasbordDelegate));
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped_1(sender, e);
        }

        [Obsolete]
        protected override void OnAppearing()
        {
            delyveryMV.Init();
        }
    }
}