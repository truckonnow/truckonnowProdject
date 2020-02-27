using MDispatch.iOS.NewRender.Labal;
using MDispatch.NewElement.Labal;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabalStandart), typeof(LabalStandartRender))]
namespace MDispatch.iOS.NewRender.Labal
{
    public class LabalStandartRender : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);
        }
    }
}