
namespace MDispatch.ViewModels.InspectionMV.PickedUpMV.TypeVe
{
    public class TestCar : ITypeScan
    {
        public double GetCordinatX(string indexPhoto, double x)
        {
            double x1 = 0;
            double[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            if (maxMinForYAndX != null)
            {
                double differenceX = maxMinForYAndX[2] - maxMinForYAndX[0];
                x1 = (differenceX * x) + maxMinForYAndX[0];
            }
            return x1;
        }

        public double GetCordinatY(string indexPhoto, double y)
        {
            double y1 = 0;
            double[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            if (maxMinForYAndX != null)
            {
                double differenceY = maxMinForYAndX[3] - maxMinForYAndX[1];
                y1 = (differenceY * y) + maxMinForYAndX[1];
            }
            return y1;
        }

        private double[] GetMaxMinForYAndX(string indexPhoto)
        {
            double[] maxMinForYAndX = null;
            if(indexPhoto == "1")
            {
                maxMinForYAndX = new double[] {0.78, 0.23,  0.237 , 0.39 };
            }
            else if(indexPhoto == "2")
            {
                maxMinForYAndX = new double[] { 0.69, 0.29, 0.6, 0.37 };
            }
            else if(indexPhoto == "3")
            {
                maxMinForYAndX = new double[] { 0.78, 0.29, 0.69, 0.37 };
            }
            else if(indexPhoto == "4")
            {
                maxMinForYAndX = new double[] { 0.6, 0.23, 0.48, 0.37 };
            }
            else if(indexPhoto == "5")
            {
                maxMinForYAndX = new double[] { 0.48, 0.23, 0.237, 0.37 };
            }
            else if(indexPhoto == "6")
            {
                maxMinForYAndX = new double[] { 0.237, 0.741, 0.78, 0.597 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new double[] { 0.69, 0.678, 0.78, 0.617 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new double[] { 0.6, 0.678, 0.69, 0.617 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new double[] { 0.6, 0.678, 0.69, 0.617 };
            }
            return maxMinForYAndX;
        }
    }
}