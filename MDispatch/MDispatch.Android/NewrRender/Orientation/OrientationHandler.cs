using Android.App;
using Android.Content.PM;
using MDispatch.Droid.NewrRender.Orientation;
using MDispatch.NewElement;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationHandler))]
namespace MDispatch.Droid.NewrRender.Orientation
{
    public class OrientationHandler : IOrientationHandler
    {
        public void ForceLandscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void ForceSensor()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Sensor;
        }
    }
}