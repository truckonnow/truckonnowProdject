using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 300, 103, 180, 10 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 257, 85, 278, 70 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 257, 85, 278, 70 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 295, 75, 391, 10 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 347, 1, 411, 63 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 391, 0, 450, 75 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 100, 422, 67, 455 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 100, 422, 67, 455 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 255, 430, 52, 500 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 222, 325, 82, 440 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 340, 93, 230, 215 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 239, 422, 206, 455 };
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 239, 422, 206, 455 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 391, 300, 450, 235 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 347, 307, 411, 245 };
            }
            else if (indexPhoto == "26")
            {
                maxMinForYAndX = new int[] { 295, 295, 391, 230 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 310, 295, 180, 202 };
            }
            else if (indexPhoto == "28")
            {
                maxMinForYAndX = new int[] { 257, 238, 278, 220 };
            }
            else if (indexPhoto == "29")
            {
                maxMinForYAndX = new int[] { 257, 238, 278, 220 };
            }
            else if (indexPhoto == "30")
            {
                maxMinForYAndX = new int[] { 202, 295, 73, 210 };
            }
            else if (indexPhoto == "31")
            {
                maxMinForYAndX = new int[] { 170, 307, 105, 246 };
            }
            else if (indexPhoto == "32")
            {
                maxMinForYAndX = new int[] { 450, 307, 73, 202 };
            }
            else if (indexPhoto == "33")
            {
                maxMinForYAndX = new int[] { 241, 48, 182, 75 };
            }
            else if (indexPhoto == "35")
            {
                maxMinForYAndX = new int[] { 80, 250, 1, 58 };
            }
            else if (indexPhoto == "36")
            {
                maxMinForYAndX = new int[] { 75, 126, 48, 67 };
            }
            else if (indexPhoto == "37")
            {
                maxMinForYAndX = new int[] { 202, 95, 73, 1 };
            }
            else if (indexPhoto == "38")
            {
                maxMinForYAndX = new int[] { 170, 63, 105, 1 };
            }
            else if (indexPhoto == "39")
            {
                maxMinForYAndX = new int[] { 450, 106, 73, 1 };
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
                    img2 = img2.GetThumbnailImage((int)((double)damage.WidthDamage * 0.30), (int)((double)damage.HeightDamage * 0.30), null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    g.DrawImage(img1, 0, 0);
                    if (photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 24 || photoInspection.IndexPhoto == 25
                        || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 27 || photoInspection.IndexPhoto == 28 || photoInspection.IndexPhoto == 29 || photoInspection.IndexPhoto == 30 || photoInspection.IndexPhoto == 31 || photoInspection.IndexPhoto == 32 || photoInspection.IndexPhoto == 36 
                        || photoInspection.IndexPhoto == 37 || photoInspection.IndexPhoto == 38 || photoInspection.IndexPhoto == 39)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 23 || photoInspection.IndexPhoto == 33
                        || photoInspection.IndexPhoto == 21)
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
                    Image img2 = Bitmap.FromFile($"../Damages/Damage{damage.TypeCurrentStatus}{damage.IndexDamage}.jpg");
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