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
                this.Control.ContentEdgeInsets = new UIEdgeInsets(5, 10, 5, 10);
                if (Control.TitleLabel.Font.PointSize <= 15)
                {
                    Control.TitleLabel.Font = Control.Font.WithSize(Control.TitleLabel.Font.PointSize + 5);
                }
            }
        }
    
    }
}