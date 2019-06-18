using Xamarin.Forms;

namespace MDispatch.NewElement.CustomVideoCam
{
    public class VideoCameraPage : ContentPage
    {
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);
        public event PhotoResultEventHandler OnPhotoResult;

        public void SetPhotoResult(byte[] video, int width = -1, int height = -1)
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs(video, width, height));
        }

        public bool IsPreviewing { get; set; }
        public bool IsRecording { get; set; }
    }
}