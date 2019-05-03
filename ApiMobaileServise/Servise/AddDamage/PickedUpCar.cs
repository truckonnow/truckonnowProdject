using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DaoModels.DAO.Models;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class PickedUpCar : ITypeScan
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
                maxMinForYAndX = new int[] { 410, 680, 150, 785 };
            }
            else if (indexPhoto == "2")
            {
                maxMinForYAndX = new int[] { 400, 783, 360, 848 };
            }
            else if (indexPhoto == "3")
            {
                maxMinForYAndX = new int[] { 200, 783, 155, 848 };
            }
            else if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 395, 783, 165, 585 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 795, 143, 660, 35 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 660, 180, 600, 35 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 600, 200, 530, 35 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 530, 200, 420, 35 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 420, 135, 305, 35 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 305, 135, 135, 35 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 335, 125, 425, 1 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 240, 135, 325, 1 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 135, 135, 240, 1 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 135, 425, 305, 525 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 305, 425, 420, 525 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 420, 365, 530, 525 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 530, 365, 660, 525 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 600, 365, 660, 525 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 660, 365, 795, 525 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 420, 780, 330, 890 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 330, 780, 230, 890 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 230, 780, 145, 890 };
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 655, 160, 630, 120 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 655, 400, 630, 440 };
            }
            else if (indexPhoto == "26")
            {
                maxMinForYAndX = new int[] { 655, 160, 630, 120 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 655, 400, 630, 440 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 190, 590, 370, 430 };
            }
            else if (indexPhoto == "28")
            {
                maxMinForYAndX = new int[] { 685, 0, 770, 90 };
            }
            else if (indexPhoto == "29")
            {
                maxMinForYAndX = new int[] { 220, 0, 310, 90 };
            }
            else if (indexPhoto == "31")
            {
                maxMinForYAndX = new int[] { 685, 480, 770, 570 };
            }
            else if (indexPhoto == "30")
            {
                maxMinForYAndX = new int[] { 220, 480, 310, 570 };
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
                    if (photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 14 
                        || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16 || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 23 || photoInspection.IndexPhoto == 24
                        || photoInspection.IndexPhoto == 25 || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 28 || photoInspection.IndexPhoto == 29 || photoInspection.IndexPhoto == 30 || photoInspection.IndexPhoto == 31)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 1 || photoInspection.IndexPhoto == 2 || photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 13
                        || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 27)
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
            if(damageForUsers != null)
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