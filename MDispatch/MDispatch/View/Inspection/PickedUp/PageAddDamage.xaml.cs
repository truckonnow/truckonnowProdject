using MDispatch.NewElement.ResIzeImage;
using MDispatch.View.PageApp;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using Rg.Plugins.Popup.Services;
using System;
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
                    ((ImgResize)view).TouchAction += MoveTouch;
                    ((ImgResize)view).OneTabAction += RemovedDamag;
                }
            }
        }

        private async void TouchImage_TouchAction(object sender, NewElement.TouchCordinate.TouchActionEventArgs e)
        {
            stateSelect = 1;
            await PopupNavigation.PushAsync(new DamageSelecter(FullPagePhotoMV, this), true);
            await WaiteSelectDamage();
            await PopupNavigation.PopAsync(true);
            stateSelect = 1;
            await Navigation.PushAsync(new CameraPagePhoto(FullPagePhotoMV, null, fullPagePhoto, this));
            await WaiteSelectDamage();
            if (stateSelect == 0)
            {
                ImgResize image = new ImgResize()
                {
                    Source = $"DamageP{indexSelectDamage}.png",
                    WidthRequest = 15,
                    HeightRequest = 15,
                };
                image.TouchAction += MoveTouch;
                image.OneTabAction += RemovedDamag;
                AbsoluteLayout.SetLayoutBounds(image, new Rectangle(e.XInterest * 0.0001, e.YInterest * 0.0001, 15, 15));
                AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
                absla.Children.Add(image);
                await Task.Run(() =>
                {
                    FullPagePhotoMV.SetDamage(nameDamage, indexSelectDamage, prefNameDamage, e.XInterest * 0.0001, e.YInterest * 0.0001, 20, 20, image, FullPagePhotoMV.AllSourseImage.Last());
                });
            }
            else
            {
                stateSelect = 0;
            }
        }

        private void RemovedDamag(object sender)
        {
            absla.Children.Remove((Image)sender);
            ((ImgResize)sender).TouchAction -= MoveTouch;
            FullPagePhotoMV.RemmoveDamage((Image)sender);
        }

        private void MoveTouch(object sender, TouchActionEventArgs e)
        {
            ImgResize rezizeImgnew = (ImgResize)sender;
            Rectangle rectangle = AbsoluteLayout.GetLayoutBounds(rezizeImgnew);
            rectangle.Height += e.IncreasePerUnit;
            rectangle.Width += e.IncreasePerUnit;
            if (rectangle.Height > 15 && rectangle.Height < 100)
            {
                AbsoluteLayout.SetLayoutBounds(rezizeImgnew, rectangle);
                Task.Run(() =>
                {
                    FullPagePhotoMV.ReSetDamage((Image)sender, (int)rectangle.Width, (int)rectangle.Height);
                });
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (absla.Children.ToList().Count - 1 > 2)
            {
                List<Xamarin.Forms.View> views = absla.Children.ToList().GetRange(3, absla.Children.ToList().Count - 3);
                foreach (var view in views)
                {
                    try
                    {
                        ((ImgResize)view).TouchAction -= MoveTouch;
                        ((ImgResize)view).OneTabAction -= RemovedDamag;
                    }
                    catch
                    { }
                    fullPagePhoto.AddDamagCurrentLayut(view);
                }
            }
            return base.OnBackButtonPressed();
        }

        private async Task WaiteSelectDamage()
        {
            await Task.Run(() => { while(stateSelect == 1) { } });
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (absla.Children.ToList().Count - 1 > 2)
            {
                List<Xamarin.Forms.View> views = absla.Children.ToList().GetRange(3, absla.Children.ToList().Count - 3);
                foreach (var view in views)
                {
                    try
                    {
                        ((ImgResize)view).TouchAction -= MoveTouch;
                        ((ImgResize)view).OneTabAction -= RemovedDamag;
                    }
                    catch
                    { }
                    fullPagePhoto.AddDamagCurrentLayut(view);
                }
            }
            await Navigation.PopAsync();
        }
    }
}