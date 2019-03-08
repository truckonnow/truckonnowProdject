using MDispatch.NewElement;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.InspectionMV.DelyveryMV;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAddDamageFoUser : ContentPage
	{
        private AskForUsersDelyveryMW askForUsersDelyveryMW = null;
        public int stateSelect = 0;
        public int indexSelectDamage = 0;
        public string nameDamage = null;
        public string prefNameDamage = null;
        private ImageSource imageSource = null;
        private StackLayout stackLayout = null;

        public PageAddDamageFoUser(AskForUsersDelyveryMW askForUsersDelyveryMW, ImageSource imageSource, StackLayout stackLayout)
        {
            this.stackLayout = stackLayout;
            this.askForUsersDelyveryMW = askForUsersDelyveryMW;
            this.imageSource = imageSource;
            InitializeComponent();
            touchImage.Source = $"scan{askForUsersDelyveryMW.VehiclwInformation.Ask.TypeVehicle.Replace(" ", "")}";
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void TouchImage_TouchAction(object sender, NewElement.TouchCordinate.TouchActionEventArgs e)
        {
            stateSelect = 1;
            await PopupNavigation.PushAsync(new DamageSelecter1(this, null), true);
            await WaiteSelectDamage();
            await PopupNavigation.PopAsync(true);
            if (stateSelect == 0)
            {
                Image image = new Image()
                {
                    Source = $"DamageD{indexSelectDamage}.png",
                    WidthRequest = 15,
                    HeightRequest = 15,
                };
                image.GestureRecognizers.Add(new TapGestureRecognizer(RemovedDamag));
                AbsoluteLayout.SetLayoutBounds(image, new Rectangle(e.XInterest * 0.0001, e.YInterest * 0.0001, 15, 15));
                AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
                absla.Children.Add(image);
                await Task.Run(() =>
                {
                    askForUsersDelyveryMW.SetDamage(nameDamage, indexSelectDamage, prefNameDamage, e.XInterest * 0.0001, e.YInterest * 0.0001, image, imageSource);
                });
                await Navigation.PopAsync();
            }
            else
            {
                stateSelect = 0;
            }
        }

        private async void RemovedDamag(Xamarin.Forms.View v, object s)
        {
            absla.Children.Remove(v);
            askForUsersDelyveryMW.RemmoveDamage((Image)v, stackLayout);
        }

        private async Task WaiteSelectDamage()
        {
            await Task.Run(() =>
            {
                while(stateSelect == 1)
                { }
            });
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}