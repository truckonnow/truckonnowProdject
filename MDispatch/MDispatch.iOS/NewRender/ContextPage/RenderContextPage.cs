using System;
using FormsControls.Base;
using MDispatch.iOS.NewRender.ContextPage;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AnimationNavigationPage), typeof(RenderContextPage))]
namespace MDispatch.iOS.NewRender.ContextPage
{
    public class RenderContextPage : NavigationRenderer
    {
        public RenderContextPage()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (InteractivePopGestureRecognizer != null)
            {
                InteractivePopGestureRecognizer.Enabled = false;
            }
        }
    }
}