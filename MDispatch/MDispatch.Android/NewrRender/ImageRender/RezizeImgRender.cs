using Android.Content;
using Android.Views;
using MDispatch.Droid.NewrRender.ImageRender;
using MDispatch.NewElement.ResIzeImage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImgResize), typeof(RezizeImgRender))]
namespace MDispatch.Droid.NewrRender.ImageRender
{
    class RezizeImgRender : ImageRenderer
    {

        public RezizeImgRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {
                var imageView = new Android.Widget.ImageButton(Context);
                this.SetNativeControl(imageView);
            }
            Control.Touch += OnTouch;
        }
        
        private double x = 0;
        private void OnTouch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Down)
            {
                x = e.Event.GetX();
            }
            else if (e.Event.Action == MotionEventActions.Move)
            {
                double newX = e.Event.GetX();
                int increasePerUnit = Convert.ToInt32(Math.Round(newX - x, 0));
                if (increasePerUnit > 0)
                {
                    increasePerUnit = 2;
                }
                else
                {
                    increasePerUnit = -2;
                }
                if (increasePerUnit != 0)
                {
                    NewElement.ResIzeImage.TouchActionEventArgs TouchActionEventArgs = new NewElement.ResIzeImage.TouchActionEventArgs(increasePerUnit);
                    (Element as ImgResize).FireClick(TouchActionEventArgs);
                }
            }
            else if(e.Event.Action == MotionEventActions.Up)
            {
                double newX = e.Event.GetX();
                int increasePerUnit = Convert.ToInt32(Math.Round(newX - x, 0));
                if (increasePerUnit == 0)
                {
                    (Element as ImgResize).FireClick();
                }
            }
        }
    }
}