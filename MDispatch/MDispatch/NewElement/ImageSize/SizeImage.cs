using Xamarin.Forms;

namespace MDispatch.NewElement.ImageSize
{
    public class SizeImage : Image
    {
        public event LoadImageActionEventHandler LoadImageAction;

        public void LoadImage(LoadImageActionEventArgs e)
        {
            if (this.LoadImageAction != null)
                this.LoadImageAction(this, e);
        }
    }
}