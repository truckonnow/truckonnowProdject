using Android.Content;
using MDispatch.Droid.NewrRender.Label;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CorectLabel))]
namespace MDispatch.Droid.NewrRender.Label
{
    public class CorectLabel : LabelRenderer
    {
        public CorectLabel(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);
            var element = this.Element as Xamarin.Forms.Label;
            if (element != null && element.Text != null && element.Text != "")
            {
                this.Element.Text = this.Element.Text.ToUpper();
            }
        }
    }
}