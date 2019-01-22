using Android.Views;
using Android.Widget;
using MDispatch.Droid.NewrRender.TouchCordinateRender;
using MDispatch.NewElement.TouchCordinate;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TouchImage), typeof(TouchImageRender))]
namespace MDispatch.Droid.NewrRender.TouchCordinateRender
{
    public class TouchImageRender : ImageRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
            {

                var imageView = new ImageButton(Context);
                this.SetNativeControl(imageView);
            }
            Control.Touch += OnTouch;
        }
        
        private void OnTouch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                Point point = new Point(e.Event.GetX(), e.Event.GetY());
                double xInterest = (e.Event.GetX() / Control.Width) * 10000;
                double yInterest = (e.Event.GetY() / Control.Height) * 10000;

                MDispatch.NewElement.TouchCordinate.TouchActionEventArgs TouchActionEventArgs = new TouchActionEventArgs(point, Control.Height, Control.Width, xInterest, yInterest);
                (Element as TouchImage).FireClick(TouchActionEventArgs);
            }
        }
    }
}