using Android.Widget;
using MDispatch.Droid.NewrRender.ToastNotify;
using MDispatch.NewElement.ToastNotify;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastDroid))]
namespace MDispatch.Droid.NewrRender.ToastNotify
{
    class ToastDroid : IToast
    {
        public void ShowMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}