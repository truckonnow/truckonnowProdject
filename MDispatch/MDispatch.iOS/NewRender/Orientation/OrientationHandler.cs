using System;
using Foundation;
using MDispatch.iOS.NewRender.Orientation;
using MDispatch.NewElement;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationHandler))]    
namespace MDispatch.iOS.NewRender.Orientation
{
    public class OrientationHandler : IOrientationHandler
    {
        public void ForceLandscape()
        {
            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.LandscapeLeft)), new NSString("orientation"));
        }

        public void ForcePortrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation")); 
        }

        public void ForceSensor()
        {
            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Unknown)), new NSString("orientation"));
        }
    }
}
