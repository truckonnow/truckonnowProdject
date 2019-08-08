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
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });
                UNUserNotificationCenter.Current.Delegate = this;
            }
            else
            {
                // iOS 9 <=
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            //OnTokenRefresh();
            Firebase.Core.App.Configure();
            Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) => {
                ManagerStore managerStore = new ManagerStore();
                var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
                managerStore.SendTokenStore(newToken);
            });
            return base.FinishedLaunching(app, options);
            //Window.RootViewController = new UINavigationController(Window.RootViewController);
        }

        public UIInterfaceOrientationMask CurrentOrientation = UIInterfaceOrientationMask.All;

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            return CurrentOrientation;
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {

        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {

        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {

        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            NSString title = ((userInfo["aps"] as NSDictionary)["alert"] as NSDictionary)["title"] as NSString;
            NSString message = ((userInfo["aps"] as NSDictionary)["alert"] as NSDictionary)["body"] as NSString;
        }

        public void OnTokenRefresh()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });
                UNUserNotificationCenter.Current.Delegate = this;
            }
            else
            {
                // iOS 9 <=
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            ManagerStore managerStore = new ManagerStore();
            var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
            managerStore.SendTokenStore(newToken);
        }
    }
}
