using System;
using Firebase.CloudMessaging;
using Foundation;
using MDispatch.iOS;
using MDispatch.StoreNotify;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: Xamarin.Forms.Dependency(typeof(AppDelegate))]
namespace MDispatch.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate, IStore
    {
        [System.Obsolete]
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            FormsControls.Touch.Main.Init();
            UINavigationBar.Appearance.BarTintColor = Color.FromHex("#4fd2c2").ToUIColor();
            LoadApplication(new App());
            Firebase.Core.App.Configure();

            return base.FinishedLaunching(app, options);
        }

        

        public void OnTokenRefresh()
        {
            
            ManagerStore managerStore = new ManagerStore();
            var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
            managerStore.SendTokenStore(newToken);
        }
    }
}