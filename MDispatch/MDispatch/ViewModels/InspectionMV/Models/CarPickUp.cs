using MDispatch.ViewModels.AskPhoto;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarPickUp : ICar
    {
        public string typeIndex { get; set; } = "PickUp";
        public int CountCarImg { get; set; }

        public string GetNameLayout(int inderxPhotoInspektion)
        {
            throw new System.NotImplementedException();
        }

        public async void OrintableScreen(int inderxPhotoInspektion)
        {
            throw new System.NotImplementedException();
        }
    }
}
