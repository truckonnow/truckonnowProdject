using MDispatch.View.Inspection.PickedUp;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDispatch.Service.GeloctionGPS
{
    public static class Utils
    {
        public static async Task StartListening(bool isTwoConection = false)
        {
            if (CrossGeolocator.Current.IsListening)
            {
                return;
            }
            try
            {
                bool s = await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 1, true);
                CrossGeolocator.Current.PositionChanged += PositionChanged;
            }
            catch(GeolocationException)
            {
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if(permissionStatus == PermissionStatus.Denied)
                {
                    if(isTwoConection)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        await PopupNavigation.PushAsync(new WarningModalPage());
                    }
                }
            }
        }

        private static void PositionChanged(object sender, PositionEventArgs e)
        {

        }


        public static async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
            {
                return;
            }
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= PositionChanged;
        }
    }
}
