using Android.Content;
using MDispatch.Droid.NewrRender.ImageRender;
using MDispatch.NewElement.ImageSize;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SizeImage), typeof(ImageSizeRender))]
namespace MDispatch.Droid.NewrRender.ImageRender
{
    class ImageSizeRender : ImageRenderer
    {
        public ImageSizeRender(Context context) : base(context)
        {

        }

        protected override async void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null || Element == null) return;
            await CheckedLoadElement();
        }

        private async Task CheckedLoadElement()
        {
            await Task.Run(() => { while (Control.Height == 0 && Control.Width == 0) { } });
            LoadImageActionEventArgs loadImageActionEventArgs = new LoadImageActionEventArgs(Control.Height, Control.Width);
            (Element as SizeImage).LoadImage(loadImageActionEventArgs);
        }
    }
}