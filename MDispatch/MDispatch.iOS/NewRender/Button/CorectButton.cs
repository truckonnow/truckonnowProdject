using MDispatch.iOS.NewRender.Button;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(CorectButton))]
namespace MDispatch.iOS.NewRender.Button
{
    public class CorectButton : ButtonRenderer
    {
        public CorectButton()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            UpdatePadding();
        }

        private void UpdatePadding()
        {
            var element = this.Element as Xamarin.Forms.Button;
            if (element != null)
            {
                this.Element.Text = this.Element.Text.ToUpper();
                this.Element.Padding = new Thickness(6, 4, 6, 4);
            }
        }
    
    }
}