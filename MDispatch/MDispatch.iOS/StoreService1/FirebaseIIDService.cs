using Firebase.CloudMessaging;
using MDispatch.iOS.StoreService1;
using MDispatch.StoreNotify;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseIIDService))]
namespace MDispatch.iOS.StoreService1
{
    public class FirebaseIIDService : IStore
    {

        public void OnTokenRefresh()
        {
            ManagerStore managerStore = new ManagerStore();
            managerStore.SendTokenStore(Messaging.SharedInstance.FcmToken);
        }
    }
}
