using Android.App;
using MDispatch.NewElement.StoreTocken;

namespace MDispatch.Droid.PlayMarket
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class MyFirebaseIIDService : FirebaseInstanceIdService
    {

        public override void OnTokenRefresh()
        {
            ManagerStore managerStore = null;
            object FirebaseInstanceId = null;
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            base.OnTokenRefresh();
        }
    }
}