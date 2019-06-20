using MDispatch.ViewModels.InspectionMV.DelyveryMV;

namespace MDispatch.ViewModels.InspectionMV.Servise.Retake
{
    class RetakeFullPageDelivery : IRetake
    {
        private FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV = null;
        private byte[] olPhoto = null;

        public RetakeFullPageDelivery(FullPagePhotoDelyveryMV fullPagePhotoDelyveryMV, byte[] olPhoto)
        {
            this.fullPagePhotoDelyveryMV = fullPagePhotoDelyveryMV;
            this.olPhoto = olPhoto;
        }

        public void SetRetakePhoto(byte[] photo)
        {
            fullPagePhotoDelyveryMV.ReSetPhoto(photo, olPhoto);
        }
    }