using System;
using CoreGraphics;
using MDispatch.iOS.TouchCordinateRender;
using MDispatch.NewElement.TouchCordinate;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TouchImage), typeof(TouchImageRender))]
namespace MDispatch.iOS.TouchCordinateRender
{
    public class TouchImageRender : ImageRenderer
    {
        private UIImageView nativeElement;
        private TouchImage touchImage;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                touchImage = e.NewElement as TouchImage;
                nativeElement = Control as UIImageView;
                nativeElement.UserInteractionEnabled = true;
                UITapGestureRecognizer tgr = new UITapGestureRecognizer(TapHandler);
                nativeElement.AddGestureRecognizer(tgr);
            }
        }

        public void TapHandler(UITapGestureRecognizer tgr)
        {
            CGPoint touchPoint = tgr.LocationInView(nativeElement);
            Point point = new Point((double)touchPoint.X, (double)touchPoint.Y);
            double xInterest = ((double)touchPoint.X / nativeElement.Frame.Size.Width) * 10000;
            double yInterest = ((double)touchPoint.Y / nativeElement.Frame.Size.Height) * 10000;
            MDispatch.NewElement.TouchCordinate.TouchActionEventArgs TouchActionEventArgs = new TouchActionEventArgs(point, nativeElement.Frame.Size.Height, nativeElement.Frame.Size.Width, xInterest, yInterest);
            (Element as TouchImage).FireClick(TouchActionEventArgs);
        }

    }
}