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
	public partial class PageAddDamage1 : ContentPage
	{
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        public int stateSelect = 0;
        public int indexSelectDamage = 0;
        public string nameDamage = null;
        public string prefNameDamage = null;
        private FullPagePhotoDelyvery fullPagePhotoDelyvery = null;

        public PageAddDamage1(FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV, FullPagePhotoDelyvery fullPagePhotoDelyvery, List<Xamarin.Forms.View> views = null)
        {
            this.fullPagePhotoDelyvery = fullPagePhotoDelyvery;
            this.fullPagePhotoDelyveryMV = fullPagePhotoDelyveryMV;
            InitializeComponent();
            touchImage.Source = this.fullPagePhotoDelyveryMV.SourseImage;
            NavigationPage.SetHasNavigationBar(this, false);
            if (views != null)
            {
                foreach (var view in views)
                {
                    view.GestureRecognizers.Clear();
                    absla.Children.Add(view);
                    view.GestureRecognizers.Add(new TapGestureRecognizer(RemovedDamag));
                }
            }
        }

        private async void TouchImage_TouchAction(object sender, NewElement.TouchCordinate.TouchActionEventArgs e)
        {
            stateSelect = 1;
            await PopupNavigation.PushAsync(new DamageSelecter1(null, this), true);
            await WaiteSelectDamage();
            await PopupNavigation.PopAsync(true);
            stateSelect = 1;
            await Navigation.PushAsync(new CameraPagePhoto1(fullPagePhotoDelyveryMV, null, fullPagePhotoDelyvery, this));
            await WaiteSelectDamage();
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
                    fullPagePhotoDelyveryMV.SetDamage(nameDamage, indexSelectDamage, prefNameDamage, e.XInterest * 0.0001, e.YInterest * 0.0001, image, fullPagePhotoDelyveryMV.AllSourseImage.Last());
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
            this.fullPagePhotoDelyveryMV.RemmoveDamage((Image)v);
        }

        protected override bool OnBackButtonPressed()
        {
            List<Xamarin.Forms.View> views = absla.Children.ToList().GetRange(1, absla.Children.ToList().Count - 1);
            foreach (var view in views)
            {
                view.GestureRecognizers.Clear();
                fullPagePhotoDelyvery.AddDamagCurrentLayut(view);
            }
            return base.OnBackButtonPressed();
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
            if (absla.Children.ToList().Count - 1 > 2)
            {
                List<Xamarin.Forms.View> views = absla.Children.ToList().GetRange(2, absla.Children.ToList().Count - 2);
                foreach (var view in views)
                {
                    view.GestureRecognizers.Clear();
                    fullPagePhotoDelyvery.AddDamagCurrentLayut(view);
                }
            }
            await Navigation.PopAsync();
        }
    }
}