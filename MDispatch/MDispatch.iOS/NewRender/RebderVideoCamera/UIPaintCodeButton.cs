using System;
using CoreGraphics;
using UIKit;

namespace MDispatch.iOS.NewRender.RebderVideoCamera
{
    public class UIPaintCodeButton : UIButton
    {
        Action<CGRect> _drawing;
        public UIPaintCodeButton(Action<CGRect> drawing)
        {
            _drawing = drawing;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            _drawing(rect);
        }
    }
}
