using Android.App;
using Android.Content.PM;
using Android.OS;

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
            typeof(System.ComponentModel.INotifyPropertyChanging).GetHashCode();
            typeof(System.ComponentModel.INotifyPropertyChanged).GetHashCode();
            LoadApplication(new App());
        }
    }
}

