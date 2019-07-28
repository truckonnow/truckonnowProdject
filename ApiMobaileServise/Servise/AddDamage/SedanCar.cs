using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using DaoModels.DAO.Models;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class SedanCar : ITypeScan
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
            if (indexPhoto == "1")
            {
                maxMinForYAndX = new int[] { 447, 75, 320, 1 };
            }
            else if (indexPhoto == "2")
            {
                maxMinForYAndX = new int[] { 315, 115, 225, 1 };
            }
            else if (indexPhoto == "3")
            {
                maxMinForYAndX = new int[] { 230, 115, 135, 1 };
            }
            else if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 140, 80, 55, 1 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 55, 235, 135, 315 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 140, 207, 230, 315 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 225, 207, 315, 315 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 320, 235, 447, 315 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 340, 275, 400, 370 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 107, 275, 167, 370 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 107, 48, 167, 0 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 340, 48, 400, 0 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 190, 430, 227, 455 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 130, 430, 93, 63 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 280, 85, 310, 65 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 280, 237, 310, 257 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 280, 237, 310, 257 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 280, 85, 310, 65 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 220, 345, 100, 280 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 227, 420, 190, 490 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 190, 420, 135, 490 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 135, 420, 95, 490 };
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 227, 420, 95, 345 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 110, 280, 210, 135 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 95, 75, 130, 0 };
            }
            else if (indexPhoto == "26")
            {
                maxMinForYAndX = new int[] { 130, 75, 195, 0 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 195, 75, 225, 0 };
            }
            return maxMinForYAndX;
        }

        public async Task SetDamage(PhotoInspection photoInspection, string typrCar, string pathScan)
        {

            if (photoInspection.Damages != null)
            {
                foreach (var damage in photoInspection.Damages)
                {
                    Image img1 = Bitmap.FromFile(pathScan);
                    Image img2 = Bitmap.FromFile($"../Damages/Damage{damage.TypeCurrentStatus}{damage.IndexDamage}.png");
                    img2 = img2.GetThumbnailImage(damage.WidthDamage, damage.HeightDamage, null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    g.DrawImage(img1, 0, 0);
                    if (photoInspection.IndexPhoto == 1 || photoInspection.IndexPhoto == 2 || photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7
                        || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16
                         || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 )
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 23
                        || photoInspection.IndexPhoto == 24 || photoInspection.IndexPhoto == 25 || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 27)
                    {
                        g.DrawImage(img2, y, x);
                    }
                    string tempPath = pathScan + "1";
                    res.Save($"{pathScan.Replace(".jpg", "")}1.jpg");
                    img1.Dispose();
                    res.Dispose();
                    g.Dispose();
                    img1 = null;
                    res = null;
                    g = null;
                    File.Delete(pathScan);
                    File.Move($"{pathScan.Replace(".jpg", "")}1.jpg", pathScan);
                }
            }
        }

        public async Task SetDamage(List<DamageForUser> damageForUsers, string typrCar, string pathScan)
        {
            if (damageForUsers != null)
            {
                foreach (var damage in damageForUsers)
                {
                    Image img1 = Bitmap.FromFile(pathScan);
                    Image img2 = Bitmap.FromFile($"../Damages/Damage{damage.TypeCurrentStatus}{damage.IndexDamage}.png");
                    img2 = img2.GetThumbnailImage(damage.WidthDamage, damage.HeightDamage, null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = (int)Math.Round(img1.Width * damage.XInterest, 0);
                    int y = (int)Math.Round(img1.Height * damage.YInterest, 0);
                    g.DrawImage(img1, 0, 0);
                    g.DrawImage(img2, x, y);
                    string tempPath = pathScan + "1";
                    res.Save($"{pathScan.Replace(".jpg", "")}1.jpg");
                    img1.Dispose();
                    res.Dispose();
                    g.Dispose();
                    img1 = null;
                    res = null;
                    g = null;
                    File.Delete(pathScan);
                    File.Move($"{pathScan.Replace(".jpg", "")}1.jpg", pathScan);
                }
            }
        }
    }
}