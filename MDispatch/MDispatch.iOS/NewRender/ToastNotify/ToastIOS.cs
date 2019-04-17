using MDispatch.iOS.NewRender.ToastNotify;
using MDispatch.NewElement.ToastNotify;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastIOS))]
namespace MDispatch.iOS.NewRender.ToastNotify
{
    class ToastIOS : IToast
    {
        ToastIOS toastIOS = new ToastIOS();
        public void ShowMessage(string message)
        {
            toastIOS.ShowMessage(message);
        }
    }
}