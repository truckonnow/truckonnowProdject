using MDispatch.Service;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.TAbbMV;
using MDispatch.ViewModels.TAbbMV.DialogAsk;
using Plugin.Settings;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.TabPage.Tab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivePage : ContentPage
    {
        public ActiveMV activeMV = null;
        private StackLayout SelectStackLayout = null;
        private InitDasbordDelegate initDasbordDelegate = null;

        public ActivePage (ManagerDispatchMob managerDispatchMob, INavigation navigation)
		{
            this.activeMV = new ActiveMV(managerDispatchMob, navigation);
			InitializeComponent ();
            BindingContext = this.activeMV;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            StackLayout stackLayout = ((StackLayout)sender).FindByName<StackLayout>("st");

            if (SelectStackLayout != null)
            {
                SelectStackLayout.BackgroundColor = Color.White;
            }
            SelectStackLayout = stackLayout;
            SelectStackLayout.BackgroundColor = Color.FromHex("#f5c8c8");
        }

        [Obsolete]
        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (!activeMV.UnTimeOfInspection.ISMaybiInspection)
            {
                await PopupNavigation.PushAsync(new AskHint(activeMV));
            }
            else
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
                //AnimationNextPage(new InfoOrder(activeMV.managerDispatchMob, activeMV.Shippings.Find(s => s.Id == idOrder), activeMV.initDasbordDelegate) { TranslationX = 500 });
                await Navigation.PushAsync(new InfoOrder(activeMV.managerDispatchMob, activeMV.Shippings.Find(s => s.Id == idOrder), activeMV.initDasbordDelegate));
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped_1(sender, e);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            activeMV.OutAccount();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Task.Delay(200).ContinueWith(t => InitElement());
        }

        public void UnInitElement()
        {
        }

        public async void InitElement()
        {
            await Task.WhenAll(
                ctp.TranslateTo(0, 0, 400, Easing.SpringOut)
                );
        }

        public void AnimationNextPage(Page page)
        {
            new Animation(a => TranslationX = a, 0, -400)
                    .Commit(this, "PageExitAnimation", 1, 400, Easing.SpringIn, (d, b) =>
                    {
                        var otherPage = page;
                        Navigation.PushAsync(otherPage, false);
                        otherPage.TranslateTo(0, 0, 400, Easing.SpringOut);
                    });
        }
    }
}