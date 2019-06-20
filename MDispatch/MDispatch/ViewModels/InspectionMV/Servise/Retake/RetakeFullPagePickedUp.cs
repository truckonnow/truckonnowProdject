using MDispatch.ViewModels.InspectionMV.PickedUpMV;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    public class RetakeFullPagePickedUp : IRetake
    {
        private FullPagePhotoMV fullPagePhotoMV = null;
        private byte[] olPhoto = null;

        public RetakeFullPagePickedUp(FullPagePhotoMV fullPagePhotoMV, byte[] olPhoto)
        {
            this.fullPagePhotoMV = fullPagePhotoMV;
            this.olPhoto = olPhoto;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            fullPagePhotoMV.ReSetPhoto(photo, olPhoto);
        }
    }
}