
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Firebase.Messaging;
using System.Collections.Generic;

namespace MDispatch.Droid.PlayMarket
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class FirebaseMessagingService : Firebase.Messaging.FirebaseMessagingService
    {
        internal static readonly string CHANNEL_ID = "Dispatch";
        internal static readonly int NOTIFICATION_ID = 1;

        public override void OnMessageReceived(RemoteMessage message)
        {
            var body = message.GetNotification();
            SendNotification(body.Body, body.Title, message.Data);
        }

        void SendNotification(string messageBody, string title, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }
            var pendingIntent = PendingIntent.GetActivity(this, NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(this, CHANNEL_ID)
                                      .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.All))
                                      .SetSmallIcon(Resource.Drawable.newOrder)
                                      .SetContentTitle(title)
                                      .SetContentText(messageBody)
                                      .SetDefaults((int)NotificationDefaults.All)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent)
                                      .SetPriority((int)NotificationPriority.Max)
                                      .SetVisibility((int)NotificationVisibility.Public)
                                      .SetCategory(Notification.CategorySocial);
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}