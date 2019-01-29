using MDispatch.View.PageApp;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAddDamage : ContentPage
	{
        private FullPagePhotoMV FullPagePhotoMV = null;
        public int stateSelect = 0;
        public int indexSelectDamage = 0;
        public string nameDamage = null;
        public string prefNameDamage = null;
        private FullPagePhoto fullPagePhoto = null;

        public PageAddDamage(FullPagePhotoMV fullPagePhotoMV, FullPagePhoto fullPagePhoto, List<Xamarin.Forms.View> views = null)
        {
            this.fullPagePhoto = fullPagePhoto;
            FullPagePhotoMV = fullPagePhotoMV;
            InitializeComponent();
            touchImage.Source = FullPagePhotoMV.SourseImage;
            NavigationPage.SetHasNavigationBar(this, false);
            if (views != null)
            {
                foreach (var view in views)
                {
                    absla.Children.Add(view);
                    view.GestureRecognizers.Add(new TapGestureRecognizer(RemovedDamag));
                }
            }
        }

        private async void TouchImage_TouchAction(object sender, NewElement.TouchCordinate.TouchActionEventArgs e)
        {
            stateSelect = 1;
            await PopupNavigation.PushAsync(new DamageSelecter(FullPagePhotoMV, this), true);
            await WaiteSelectDamage();
            if (stateSelect == 0)
            {
                Image image = new Image()
                {
                    Source = $"DamageP{indexSelectDamage}.png",
                    WidthRequest = 15,
                    HeightRequest = 15,
                };
                image.GestureRecognizers.Add(new TapGestureRecognizer(RemovedDamag));
                AbsoluteLayout.SetLayoutBounds(image, new Rectangle(e.XInterest * 0.0001, e.YInterest * 0.0001, 15, 15));
                AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
                absla.Children.Add(image);
                await PopupNavigation.PopAsync(true);
                await Task.Run(() =>
                {
                    FullPagePhotoMV.SetDamage(nameDamage, indexSelectDamage, prefNameDamage, e.XInterest * 0.0001, e.YInterest * 0.0001, image);
                });
            }
            else
            {
                stateSelect = 0;
            }
        }

        private async void RemovedDamag(Xamarin.Forms.View v, object s)
        {
            absla.Children.Remove(v);
            FullPagePhotoMV.RemmoveDamage((Image)v);
        }

        protected override bool OnBackButtonPressed()
        {
            List<Xamarin.Forms.View> views = absla.Children.ToList().GetRange(1, absla.Children.ToList().Count - 1);
            foreach(var view in views)
            {
                view.GestureRecognizers.Remove(new TapGestureRecognizer(RemovedDamag));
                fullPagePhoto.AddDamagCurrentLayut(view);
            }
            return base.OnBackButtonPressed();
        }

        private async Task WaiteSelectDamage()
        {
            await Task.Run(() =>
            {
                while(stateSelect == 1)
                {

                }
            });
        }
    }
}