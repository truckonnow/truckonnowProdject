using System.Threading.Tasks;

namespace MDispatch.ViewModels.AskPhoto
{
    public interface ICar
    {
        string typeIndex { get; set; }
        int CountCarImg { get; set; }
        Task OrintableScreen(int inderxPhotoInspektion);
        string GetNameLayout(int inderxPhotoInspektion);
        int GetIndexCar(int countPhoto);
        //int GetIndexCarFullPhoto(int countPhoto);
    }
}