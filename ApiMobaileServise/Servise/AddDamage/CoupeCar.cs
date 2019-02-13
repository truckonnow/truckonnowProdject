using System;
using System.Drawing;
using DaoModels.DAO.Models;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class CoupeCar : ITypeScan
    {
        public int GetCordinatX(string indexPhoto, double x)
        {
            int x1 = 0;
            int[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            if (maxMinForYAndX != null)
            {
                int differenceX = maxMinForYAndX[2] - maxMinForYAndX[0];
                x1 = Convert.ToInt32(Math.Round((differenceX * x) + maxMinForYAndX[1], 0)); ;
            }
            return x1;
        }

        public int GetCordinatY(string indexPhoto, double y)
        {
            int y1 = 0;
            int[] maxMinForYAndX = GetMaxMinForYAndX(indexPhoto);
            if (maxMinForYAndX != null)
            {
                int differenceY = maxMinForYAndX[3] - maxMinForYAndX[1];
                y1 = Convert.ToInt32(Math.Round((differenceY * y) + maxMinForYAndX[1], 0));
            }
            return y1;
        }

        public int[] GetMaxMinForYAndX(string indexPhoto)
        {
            int[] maxMinForYAndX = null;
            if (indexPhoto == "1")
            {
                maxMinForYAndX = new int[] { 78, 23, 237, 39 };
            }
            return maxMinForYAndX;
        }

        public void SetDamage(PhotoInspection photoInspection, string typrCar, string pathScan)
        {
            foreach (var damage in photoInspection.Damages)
            {
                Image img1 = Bitmap.FromFile(pathScan);
                Image img2 = Bitmap.FromFile($"Damage{damage.TypeCurrentStatus}{damage.IndexDamage}.png");
                img2 = img2.GetThumbnailImage(15, 15, null, IntPtr.Zero);
                Bitmap res = new Bitmap(img1.Width, img1.Height);
                Graphics g = Graphics.FromImage(res);
                int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                int y = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                g.DrawImage(img1, 0, 0);
                g.DrawImage(img2, x, y);
            }
        }
    }
}