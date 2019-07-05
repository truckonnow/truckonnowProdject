using MDispatch.View.AskPhoto;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    public class RetakeAsk1 : IRetake
    {
        private AskPage askPage = null;
        private Xamarin.Forms.View view = null;

        public RetakeAsk1(AskPage askPage, Xamarin.Forms.View view)
        {
            this.askPage = askPage;
            this.view = view;
        }

        public async void SetRetakePhoto(byte[] photo)
        {
            askPage.ReSetPhoto1(view, photo);
        }
    }
}