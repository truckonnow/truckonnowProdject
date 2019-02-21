using MDispatch.ViewModels.AskPhoto;

namespace MDispatch.ViewModels.InspectionMV.Models
{
    public class CarPickUp : ICar
    {
        public string typeIndex { get; set; } = "PickUp";

        public async void OrintableScreen(int inderxPhotoInspektion)
        {
            throw new System.NotImplementedException();
        }
    }
}
