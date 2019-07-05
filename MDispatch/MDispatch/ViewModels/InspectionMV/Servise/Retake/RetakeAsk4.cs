

using MDispatch.View.Inspection;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    class RetakeAsk4 : IRetake
    {
        private Ask1Page ask1Page = null;
        private Xamarin.Forms.View view = null;

        public RetakeAsk4(Ask1Page ask1Page, Xamarin.Forms.View view)
        {
            this.ask1Page = ask1Page;
            this.view = view;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            ask1Page.ReSetPhoto1(view, photo);
        }
    }
}

