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
                maxMinForYAndX = new int[] { 335, 485, 205, 605 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 815, 115, 690, 1 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 815, 485, 690, 605 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 105, 1, 505, 140 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 125, 90, 245, 135 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 350, 90, 475, 135 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 120, 470, 480, 990 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 105, 860, 505, 1000 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 400, 835, 465, 900 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 130, 835, 195, 900 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 400, 835, 465, 900 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 130, 835, 195, 900 };
            }
            else if (indexPhoto == "26")
            {
                maxMinForYAndX = new int[] { 510, 165, 535, 125 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 510, 435, 535, 470 };
            }
            else if (indexPhoto == "28")
            {
                maxMinForYAndX = new int[] { 510, 165, 535, 125 };
            }
            else if (indexPhoto == "29")
            {
                maxMinForYAndX = new int[] { 510, 435, 535, 470 };
            }
            else if (indexPhoto == "30")
            {
                maxMinForYAndX = new int[] { 415, 650, 185, 880 };
            }
            else if (indexPhoto == "31")
            {
                maxMinForYAndX = new int[] { 415, 370, 185, 650 };
            }
            else if (indexPhoto == "35")
            {
                maxMinForYAndX = new int[] { 415, 1, 185, 400 };
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
                    if (photoInspection.IndexPhoto == 1 || photoInspection.IndexPhoto == 2 || photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6
                        || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12
                        || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 27 || photoInspection.IndexPhoto == 28
                        || photoInspection.IndexPhoto == 29)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if(photoInspection.IndexPhoto == 16 || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22
                        || photoInspection.IndexPhoto == 24 || photoInspection.IndexPhoto == 25 || photoInspection.IndexPhoto == 30 || photoInspection.IndexPhoto == 31)
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