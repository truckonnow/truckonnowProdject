
namespace MDispatch.ViewModels.InspectionMV.PickedUpMV
{
    public interface ITypeScan
    {
        double GetCordinatY(string indexPhoto, double x);
        double GetCordinatX(string indexPhoto, double y);
    }
}