using Xamarin.Forms;

namespace MDispatch.NewElement
{
    public class CameraPage : ContentPage
    {
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);
        public event PhotoResultEventHandler OnPhotoResult;
        public event PhotoResultEventHandler OnScan;
        public event PhotoResultEventHandler OnPhotoinspectionResult;

        public static readonly BindableProperty TextProperty =
           BindableProperty.Create("TypeCamera", typeof(string), typeof(CameraPage), string.Empty);

        public string TypeCamera
        {
            set
            {
                SetValue(TextProperty, value);
            }
            get
            {
                return (string)GetValue(TextProperty);
            }
        }


        public void SetPhotoResult(byte[] image, int width = -1, int height = -1)
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs(image, width, height));
        }

        public void SetScan(byte[] image, int width = -1, int height = -1)
        {
            OnScan?.Invoke(new PhotoResultEventArgs(image, width, height));
        }

        public void SetPhotoinspectionResult(byte[] image, int width = -1, int height = -1)
        {
            OnPhotoinspectionResult?.Invoke(new PhotoResultEventArgs(image, width, height));
        }

        public void Cancel()
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs());
        }
    }
}