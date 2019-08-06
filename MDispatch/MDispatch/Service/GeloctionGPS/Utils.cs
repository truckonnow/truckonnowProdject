using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Settings;
using RestSharp;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MDispatch.Service.GeloctionGPS
{
    public static class Utils
    {
        private static bool isTimeUpdate = true;

        [Obsolete]
        public static async Task StartListening(bool isTwoConection = false)
        {
            if (CrossGeolocator.Current.IsListening)
            {
                return;
            }
            try
            {
                bool s = await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 1, true);
                CrossGeolocator.Current.DesiredAccuracy = 50;
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

        private static async void PositionChanged(object sender, PositionEventArgs e)
        {
            await Task.Run(() =>
            {
                if (App.isNetwork && isTimeUpdate)
                {
                    Waite();
                    ReqvestGPS(e.Position.Longitude.ToString(), e.Position.Latitude.ToString());
                }
            });
        }

        public static void ReqvestGPS(string longitude, string latitude)
        {
            IRestResponse response = null;
            string content = null;
            try
            {
                string token = CrossSettings.Current.GetValueOrDefault("Token", "");
                RestClient client = new RestClient(Config.BaseReqvesteUrl);
                RestRequest request = new RestRequest("Mobile/GPS/Save", Method.POST);
                request.AddHeader("Accept", "application/json");
                client.Timeout = 10000;
                request.AddParameter("token", token);
                request.AddParameter("longitude", longitude);
                request.AddParameter("latitude", latitude);
                response = client.Execute(request);
                content = response.Content;
            }
            catch (Exception)
            {
            }
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

        private static async void Waite()
        {
            await Task.Run(() =>
            {
                isTimeUpdate = false;
                Thread.Sleep(120000);
                isTimeUpdate = true;
            });
        }
    }
}
