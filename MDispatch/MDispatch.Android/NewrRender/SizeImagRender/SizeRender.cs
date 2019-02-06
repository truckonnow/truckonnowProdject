using Android.Content;
using MDispatch.Droid.NewrRender.SizeImagRender;
using MDispatch.NewElement.ImageSize;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SizeImage), typeof(SizeRender))]
namespace MDispatch.Droid.NewrRender.SizeImagRender
{
    public class SizeRender : ImageRenderer
    {
        public SizeRender(Context context):base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null || Element == null) return;
            CheckedLoadedElement();
        }

        private async void CheckedLoadedElement()
        {
            await Task.Run(() => { while(Control.Height == 0 && Control.Width == 0) { } });
            LoadImageActionEventArgs loadImageActionEventArgs = new LoadImageActionEventArgs(Control.Height, Control.Width);
            (Element as SizeImage).LoadImage(loadImageActionEventArgs);
        }
    }
}