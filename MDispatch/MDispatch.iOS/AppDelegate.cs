using System;
using System.Threading.Tasks;
using AudioToolbox;
using Firebase.CloudMessaging;
using Foundation;
using MDispatch.iOS;
using MDispatch.iOS.StoreService1;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: Xamarin.Forms.Dependency(typeof(AppDelegate))]
namespace MDispatch.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        [System.Obsolete]
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            Forms.Init();
            FormsControls.Touch.Main.Init();
            UIApplication.SharedApplication.StatusBarHidden = true;
            UINavigationBar.Appearance.TintColor = Color.FromHex("7f2ed2").ToUIColor();
            UITabBar.Appearance.BarTintColor = Color.FromHex("95e3da").ToUIColor();
            UITabBar.Appearance.TintColor = Color.FromHex("7f2ed2").ToUIColor();
            LoadApplication(new App());

            Firebase.Core.App.Configure();
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound | UNAuthorizationOptions.CriticalAlert;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            Messaging.SharedInstance.Delegate = this;
            Messaging.SharedInstance.AutoInitEnabled = true;
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            return base.FinishedLaunching(app, options);
        }

        public UIInterfaceOrientationMask CurrentOrientation = UIInterfaceOrientationMask.All;

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            return CurrentOrientation;
        }
            
        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            Task.Run(() =>
            {
                FirebaseIIDService firebaseIIDService = new FirebaseIIDService();
                firebaseIIDService.OnTokenRefresh();
            });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            UIApplicationState state = UIApplication.SharedApplication.ApplicationState;
            if (state == UIApplicationState.Background)
            {
                SystemSound systemSound = new SystemSound(1003);
                systemSound.PlayAlertSound();
            }
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Sound | UNNotificationPresentationOptions.Alert);
        }
    }
}