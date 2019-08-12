using System;
using MDispatch.iOS.NewRender.ImageRender;
using MDispatch.NewElement.ResIzeImage;
using MDispatch.NewElement.TouchCordinate;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImgResize), typeof(RezizeImgRender))]
namespace MDispatch.iOS.NewRender.ImageRender
{
    public class RezizeImgRender : ImageRenderer
    {
        private UIImageView nativeElement;
        private ImgResize imgResize;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                imgResize = e.NewElement as ImgResize;
                nativeElement = Control as UIImageView;
                nativeElement.UserInteractionEnabled = true;
                UITapGestureRecognizer tgr = new UITapGestureRecognizer(TapHandler);
                nativeElement.AddGestureRecognizer(tgr);
            }
        }

        public void TapHandler(UITapGestureRecognizer tgr)
        {
            (Element as ImgResize).FireClick();
        }
    }
}
