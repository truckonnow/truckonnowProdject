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
            Messaging.SharedInstance.Delegate = this;
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{

				// For iOS 10 display notification (sent via APNS)
				UNUserNotificationCenter.Current.Delegate = this;

				var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
				UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
					Console.WriteLine(granted);
				});
			}
			else
			{
				// iOS 9 or before
				var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
				var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			}
            Messaging.SharedInstance.ShouldEstablishDirectChannel = false;
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

		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			Messaging.SharedInstance.ApnsToken = deviceToken;
        }

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			completionHandler(UIBackgroundFetchResult.NewData);
		}

       

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var userInfo = notification.Request.Content.UserInfo;
            completionHandler(UNNotificationPresentationOptions.None);
        }

        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            completionHandler();
        }

        public void OnTokenRefresh()
        {
            ManagerStore managerStore = new ManagerStore();
            var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
            managerStore.SendTokenStore(newToken);
        }
    }
}