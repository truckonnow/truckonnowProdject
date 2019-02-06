
namespace MDispatch.ViewModels.InspectionMV.PickedUpMV.TypeVe
{
    public class TestCar : ITypeScan
    {
        public double GetCordinatX(string indexPhoto, double x)
        {
            double x1 = 0;
            double[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            double differenceX = maxMinForYAndX[2] - maxMinForYAndX[0];
            x1 = (differenceX  * x) + maxMinForYAndX[0];
            return x1;
        }

        public double GetCordinatY(string indexPhoto, double y)
        {
            double y1 = 0;
            double[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            double differenceY = maxMinForYAndX[3] - maxMinForYAndX[1];
            y1 = (differenceY  * y) + maxMinForYAndX[1];
            return y1;
        }

        private double[] GetMaxMinForYAndX(string indexPhoto)
        {
            double[] maxMinForYAndX = null;
            if(indexPhoto == "1")
            {
                maxMinForYAndX = new double[] {0.237, 0.23,  0.78 , 0.39 };
            }
            return maxMinForYAndX;
        }
    }
}