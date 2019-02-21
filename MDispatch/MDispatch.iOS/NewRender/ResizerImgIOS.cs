
using CoreGraphics;
using MDispatch.iOS.NewRender;
using MDispatch.View.ServiceView.ResizeImage;
using System;
using System.Drawing;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ResizerImgIOS))]
namespace MDispatch.iOS.NewRender
{
    public class ResizerImgIOS : IResizeImage
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {
                RectangleF imageRect = new RectangleF(0, 0, width, height);
                context.DrawImage(imageRect, originalImage.CGImage);
                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);
                return resizedImage.AsJPEG().ToArray();
            }
        }

        public static UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                return null;
            }
            return image;
        }
    }
}