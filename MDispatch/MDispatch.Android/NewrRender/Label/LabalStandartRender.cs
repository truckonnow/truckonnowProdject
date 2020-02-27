using Android.Content;
using MDispatch.Droid.NewrRender.Label;
using MDispatch.NewElement.Labal;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabalStandart), typeof(LabalStandartRender))]
namespace MDispatch.Droid.NewrRender.Label
{
    public class LabalStandartRender : LabelRenderer
    {
        public LabalStandartRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);
        }
    }
}