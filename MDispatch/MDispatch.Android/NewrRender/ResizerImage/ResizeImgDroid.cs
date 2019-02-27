
using Android.Content.Res;
using Android.Graphics;
using MDispatch.Droid.NewrRender.ResizerImage;
using MDispatch.View.ServiceView.ResizeImage;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(ResizeImgDroid))]
namespace MDispatch.Droid.NewrRender.ResizerImage
{
    public class ResizeImgDroid : IResizeImage
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {

            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}