using System;
using System.Drawing;
using System.IO;
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
                x1 = Convert.ToInt32(Math.Round((differenceX * x) + maxMinForYAndX[0], 0));
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
            if (indexPhoto == "2")
            {
                maxMinForYAndX = new int[] { 900, 200, 150, 1 };
            }
            else if(indexPhoto == "3")
            {
                maxMinForYAndX = new int[] { 800, 147, 580, 14 };
            }
            else if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 900, 140, 750, 14 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 635, 200, 330, 14 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 400, 205, 140, 17 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 150, 370, 900, 610 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 750, 460, 900, 580 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 580, 470, 800, 580 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 330, 370, 635, 580 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 140, 405, 400, 580 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 335, 120, 205, 1 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 335, 120, 205, 490 };
            }
            return maxMinForYAndX;
        }

        public void SetDamage(PhotoInspection photoInspection, string typrCar, string pathScan)
        {
            if (photoInspection.Damages != null)
            {
                foreach (var damage in photoInspection.Damages)
                {
                    Image img1 = Bitmap.FromFile(pathScan);
                    Image img2 = Bitmap.FromFile($"../Damages/Damage{damage.TypeCurrentStatus}{damage.IndexDamage}.png");
                    img2 = img2.GetThumbnailImage(15, 15, null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    g.DrawImage(img1, 0, 0);
                    g.DrawImage(img2, x, y);
                    string tempPath = pathScan + "1";
                    res.Save($"{pathScan.Replace(".png", "")}1.png");
                    img1.Dispose();
                    res.Dispose();
                    g.Dispose();
                    img1 = null;
                    res = null;
                    g = null;
                    File.Delete(pathScan);
                    File.Move($"{pathScan.Replace(".png", "")}1.png", pathScan);
                }
            }
        }
    }
}