using MDispatch.View.Inspection;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    public class RetakeAsk3 : IRetake
    {
        private Ask1Page ask1Page = null;
        private Xamarin.Forms.View view = null;

        public RetakeAsk3(Ask1Page ask1Page, Xamarin.Forms.View view)
        {
            this.ask1Page = ask1Page;
            this.view = view;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            ask1Page.ReSetPhoto(view, photo);
        }
    }
}