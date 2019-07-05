using MDispatch.View.AskPhoto;
namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    class RetakeAsk2 : IRetake
    {
        private AskPage askPage = null;
        private Xamarin.Forms.View view = null;

        public RetakeAsk2(AskPage askPage, Xamarin.Forms.View view)
        {
            this.askPage = askPage;
            this.view = view;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            askPage.ReSetPhoto2(view, photo);
        }
    }
}
