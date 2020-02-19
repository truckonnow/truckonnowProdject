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

        private int CorectSetDamageY(string indexPhoto)
        {
            int yCorect = 0;
            if (indexPhoto == "1")
            {
                yCorect = 8;
            }
            else if (indexPhoto == "2")
            {
                yCorect = 2;
            }
            else if (indexPhoto == "4")
            {
                yCorect = 18;
            }
            else if (indexPhoto == "5")
            {
                yCorect = 5;
            }
            else if (indexPhoto == "6")
            {
                yCorect = 5;
            }
            else if (indexPhoto == "7")
            {
                yCorect = 5;
            }
            else if (indexPhoto == "8")
            {
                yCorect = 20;
            }
            else if (indexPhoto == "9")
            {
                yCorect = -18;
            }
            else if (indexPhoto == "10")
            {
                yCorect = -5;
            }
            else if (indexPhoto == "11")
            {
                yCorect = 5;
            }
            else if (indexPhoto == "19")
            {
                yCorect = -20;
            }
            else if (indexPhoto == "25")
            {
                yCorect = 3;
            }
            return yCorect;
        }

        private int CorectSetDamageX(string indexPhoto)
        {
            int xCorect = 0;
            if (indexPhoto == "1")
            {
                xCorect = 5;
            }
            if (indexPhoto == "2")
            {
                xCorect = 5;
            }
            //else if (indexPhoto == "3")
            //{
            //    xCorect = -10;
            //}
            else if (indexPhoto == "4")
            {
                xCorect = -6;
            }
            else if (indexPhoto == "6")
            {
                xCorect = -3;
            }
            else if (indexPhoto == "7")
            {
                xCorect = 15;
            }
            else if (indexPhoto == "8")
            {
                xCorect = 15;
            }
            else if (indexPhoto == "9")
            {
                xCorect = 8;
            }
            else if (indexPhoto == "10")
            {
                xCorect = 7;
            }
            else if (indexPhoto == "12")
            {
                xCorect = 5;
            }
            else if (indexPhoto == "12")
            {
                xCorect = 5;
            }
            else if (indexPhoto == "19")
            {
                xCorect = 10;
            }
            else if (indexPhoto == "20")
            {
                xCorect = 10;
            }
            else if (indexPhoto == "22")
            {
                xCorect = 20;
            }
            return xCorect;
        }

        public int[] GetMaxMinForYAndX(string indexPhoto)
        {
            int[] maxMinForYAndX = null;
            if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 334, 120, 232, 15 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 318, 90, 290, 72 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 318, 90, 290, 72 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 455, 87, 335, 5 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 407, 55, 350, 1 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 500, 105, 435, 145 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 135, 440, 105, 460 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 193, 435, 145, 500 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 233, 435, 193, 500 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 235, 440, 205, 460 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 105, 440, 235, 350 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 235, 290, 105, 350 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 335, 250, 450, 330 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 349, 283, 406, 338 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 230, 220, 335, 330 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 290, 248, 315, 265 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 290, 248, 315, 265 };
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 142, 220, 230, 330 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 119, 283, 172, 338 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 60, 240, 142, 325 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 190, 75, 233, 1 };
            }
            else if (indexPhoto == "28")
            {
                maxMinForYAndX = new int[] { 149, 75, 190, 1 };
            }
            else if (indexPhoto == "29")
            {
                maxMinForYAndX = new int[] { 104, 75, 149, 1 };
            }
            else if (indexPhoto == "31")
            {
                maxMinForYAndX = new int[] { 142, 100, 60, 15 };
            }
            else if (indexPhoto == "32")
            {
                maxMinForYAndX = new int[] { 172, 56, 119, 1 };
            }
            else if (indexPhoto == "33")
            {
                maxMinForYAndX = new int[] { 230, 100, 142, 15 };
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
                    if (photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 18
                        || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 23 || photoInspection.IndexPhoto == 24 || photoInspection.IndexPhoto == 25
                        || photoInspection.IndexPhoto == 31 || photoInspection.IndexPhoto == 32 || photoInspection.IndexPhoto == 33)
                    {
                        g.DrawImage(img2, x + CorectSetDamageX(photoInspection.IndexPhoto.ToString()), y + CorectSetDamageY(photoInspection.IndexPhoto.ToString()));
                    }
                    else if (photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16
                        || photoInspection.IndexPhoto == 27 || photoInspection.IndexPhoto == 28 || photoInspection.IndexPhoto == 29)
                    {
                        g.DrawImage(img2, y + CorectSetDamageY(photoInspection.IndexPhoto.ToString()), x + CorectSetDamageX(photoInspection.IndexPhoto.ToString()));
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