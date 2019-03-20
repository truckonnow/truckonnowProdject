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
                maxMinForYAndX = new int[] { 280, 680, 630, 880 };
            }
            else if (indexPhoto == "2")
            {
                maxMinForYAndX = new int[] { 320, 915, 355, 880 };
            }
            else if (indexPhoto == "3")
            {
                maxMinForYAndX = new int[] { 555, 915, 590, 880 };
            }
            else if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 300, 370, 605, 705 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 125, 595, 855, 910 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 855, 310, 125, 1 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 860, 190, 775, 65 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 775, 190, 635, 70 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 635, 310, 485, 70 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 485, 310, 350, 70 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 350, 310, 130, 70 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 1, 295, 380, 390 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 1, 390, 380, 520 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 1, 520, 380, 615 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 120, 605, 340, 860 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 340, 605, 480, 860 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 480, 605, 640, 860 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 640, 690, 780, 860 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 780, 690, 855, 860 };
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
                    if (photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9
                        || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 1 || photoInspection.IndexPhoto == 2 || photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 12
                         || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16 || photoInspection.IndexPhoto == 17
                         || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 19)
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