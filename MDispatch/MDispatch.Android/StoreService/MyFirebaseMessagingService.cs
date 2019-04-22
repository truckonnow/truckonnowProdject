using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Firebase.Messaging;

namespace MDispatch.Droid.StoreService
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        internal static readonly int NOTIFICATION_ID = 100;

        public override void OnMessageReceived(RemoteMessage message)
        {
            var body = message.GetNotification();
            SendNotification(body.Body ,body.Title, message.Data);
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
            var notificationBuilder = new NotificationCompat.Builder(this)
                                      .SetSmallIcon(Resource.Drawable.newOrder)
                                      .SetContentTitle(title)
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent)
                                      .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.All))
                                      .SetDefaults((int)NotificationDefaults.All)
                                      .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Ringtone))
                                      .SetPriority((int)NotificationPriority.High)
                                      .SetVisibility((int)NotificationVisibility.Public); 
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(1, notificationBuilder.Build());
        }
    }
}