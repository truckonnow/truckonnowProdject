using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Firebase;
using Plugin.FirebasePushNotification;
using Plugin.Permissions;
using Android.Gms.Common;
using MDispatch.Droid.PlayMarket;

namespace MDispatch.Droid
{
    [Activity(Label = "MDispatch", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            FirebaseApp.InitializeApp(Android.App.Application.Context);
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            LoadApplication(new App());
            //IsPlayServiceAvailable();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public bool IsPlayServiceAvailable()
        //{
        //    //int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
        //    //if (resultCode != ConnectionResult.Success)
        //    //{
        //    //    if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
        //    //    {
        //    //        // give the user a change to fix the issue // error message 
        //    //    }
        //    //    else
        //    //    {
        //    //        // play services not supported message 
        //    //        Finish();
        //    //    }
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //    //    // services are available message
        //    //    return true;
        //    //}
        //}

    }
}

