using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DaoModels.DAO.Models;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class SuvCar : ITypeScan
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
                maxMinForYAndX = new int[] { 900, 200, 150, 1 };
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
                    img2 = img2.GetThumbnailImage(damage.WidthDamage, damage.HeightDamage, null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    g.DrawImage(img1, 0, 0);
                    if (photoInspection.IndexPhoto == 1 || photoInspection.IndexPhoto == 2 || photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6
                        || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12
                        || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 27 || photoInspection.IndexPhoto == 28
                        || photoInspection.IndexPhoto == 29)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 16 || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22
                        || photoInspection.IndexPhoto == 24 || photoInspection.IndexPhoto == 25 || photoInspection.IndexPhoto == 30 || photoInspection.IndexPhoto == 31)
                    {
                        g.DrawImage(img2, y, x);
                    }
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

        public void SetDamage(List<DamageForUser> damageForUsers, string typrCar, string pathScan)
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
