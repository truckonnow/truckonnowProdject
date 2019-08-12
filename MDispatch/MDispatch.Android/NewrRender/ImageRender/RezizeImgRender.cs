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

        private void OnTouch(object sender, TouchEventArgs e)
        {
            if(e.Event.Action == MotionEventActions.Up)
            {
                (Element as ImgResize).FireClick();
            }
        }
    }
}