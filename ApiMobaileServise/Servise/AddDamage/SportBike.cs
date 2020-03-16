using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class SportBike : ITypeScan
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
            if (indexPhoto == "3")
            {
                maxMinForYAndX = new int[] { 480, 10, 690, 295 };
            }
            else if (indexPhoto == "4")
            {
                maxMinForYAndX = new int[] { 610, 293, 655, 375 };
            }
            else if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 560, 10, 730, 170 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 337, 670, 465, 750 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 337, 600, 465, 690 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 293, 600, 510, 907 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 340, 705, 400, 740 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 400, 705, 460, 740 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 560, 630, 730, 790 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 480, 505, 690, 790 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 610, 428, 655, 510 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 220, 10, 730, 792 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 255, 530, 480, 780 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 210, 615, 390, 792 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 210, 10, 390, 187 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 255, 20, 480, 270 };
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
                    Bitmap res = new Bitmap(img1);
                    Graphics g = Graphics.FromImage(res);
                    int x = GetCordinatX(photoInspection.IndexPhoto.ToString(), damage.XInterest);
                    int y = GetCordinatY(photoInspection.IndexPhoto.ToString(), damage.YInterest);
                    //g.DrawImage(img1, 0, 0);
                    if (photoInspection.IndexPhoto == 3 || photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 16
                         || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 19)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10)
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