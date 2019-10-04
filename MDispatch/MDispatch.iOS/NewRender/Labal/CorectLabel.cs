using MDispatch.iOS.NewRender.Labal;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(Label), typeof(CorectLabel))]
namespace MDispatch.iOS.NewRender.Labal
{
    public class CorectLabel : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);
            UpdatePadding();
        }

        private void UpdatePadding()
        {
            var element = this.Element as Xamarin.Forms.Label;
            if (element != null)
            {
                this.Element.Text = this.Element.Text.ToUpper();
            }
        }
    }
}