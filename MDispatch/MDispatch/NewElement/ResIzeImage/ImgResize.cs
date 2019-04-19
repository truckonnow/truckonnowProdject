using Xamarin.Forms;

namespace MDispatch.NewElement.ResIzeImage
{
    public class ImgResize : Image
    {
        public event OneTabActionEventHandler OneTabAction;

        public void FireClick()
        {
            if (this.OneTabAction != null)
                this.OneTabAction(this);
        }

    }
}
