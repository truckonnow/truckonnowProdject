using CoreAnimation;
using MDispatch.iOS.NewRender.Picker;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(PikerCustom))]

namespace MDispatch.iOS.NewRender.Picker
{
    public class PikerCustom : PickerRenderer
    {
        public PikerCustom()
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}
