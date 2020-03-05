using System.Threading.Tasks;

namespace MDispatch.ViewModels.InspectionMV.Servise.Models
{
    public interface IVehicle
    {
        string TypeIndex { get; set; }
        int CountCarImg { get; set; }
        Task OrintableScreen(int inderxPhotoInspektion);
        string GetNameLayout(int inderxPhotoInspektion);
        int GetIndexCar(int countPhoto);
        //int GetIndexCarFullPhoto(int countPhoto);
    }
}