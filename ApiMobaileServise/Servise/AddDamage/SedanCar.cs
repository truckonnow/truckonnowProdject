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
            if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 343, 125, 240, 15 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 328, 96, 300, 78 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 328, 96, 300, 78 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 140, 80, 55, 1 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 430, 96, 345, 15 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 407, 55, 350, 1 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 105, 500, 145, 435 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 105, 460, 135, 440 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 145, 500, 193, 435 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 193, 500, 233, 435 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 205, 460, 235, 440 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 105, 440, 235, 350 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 105, 350, 235, 290 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 105, 350, 235, 290 };
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
                    img2 = img2.GetThumbnailImage((int)((double)damage.WidthDamage * 0.30), (int)((double)damage.HeightDamage * 0.30), null, IntPtr.Zero);
                    Bitmap res = new Bitmap(img1.Width, img1.Height);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    g.DrawImage(img1, 0, 0);
                    if (photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 7)
                    {
                        g.DrawImage(img2, x + CorectSetDamageX(photoInspection.IndexPhoto.ToString()), y + CorectSetDamageY(photoInspection.IndexPhoto.ToString()));
                    }
                    else if (photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16)
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