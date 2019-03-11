using Xamarin.Forms;

namespace MDispatch.NewElement.ResIzeImage
{
    public class ImgResize : Image
    {
        public event TouchActionEventHandler TouchAction;
        public event OneTabActionEventHandler OneTabAction;

        public void FireClick(TouchActionEventArgs e)
        {
            if (this.TouchAction != null)
                this.TouchAction(this, e);
        }
        public void FireClick()
        {
            if (this.OneTabAction != null)
                this.OneTabAction(this);
        }

    }
}
