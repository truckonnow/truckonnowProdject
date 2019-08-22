using Foundation;
using MDispatch.iOS.NewRender.ToastNotify;
using MDispatch.NewElement.ToastNotify;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastIOS))]
namespace MDispatch.iOS.NewRender.ToastNotify
{
    public class ToastIOS : IToast
    {

        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowMessage(string message)
        {
            ShowAlert(message, 0.9);
        }

        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.ActionSheet);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}