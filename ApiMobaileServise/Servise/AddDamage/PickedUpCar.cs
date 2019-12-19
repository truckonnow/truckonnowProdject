using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 375, 25, 298, 110 };//
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 405, 20, 345, 105 };//
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 353, 69, 368, 96 };//
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 353, 69, 368, 96 };//
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 450, 20, 365, 85 };//
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 435, 0, 383, 50 };//
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 85, 500, 135, 435 };//
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 90, 477, 117, 438 };//
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 135, 500, 185, 435 };//
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 185, 500, 235, 435 };//
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 205, 477, 231, 437 };//
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 230, 435, 90, 378 };//
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 95, 330, 225, 382 };//
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 244, 110, 342, 210 };//
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 365, 225, 450, 300 };//
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 383, 271, 433, 321 };//
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 345, 226, 405, 305 };//
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 353, 225, 368, 252 };//
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 353, 225, 368, 252 };//
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 298, 210, 375, 300 };//
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 240, 210, 307, 300 };//
            }
            else if (indexPhoto == "26")
            {
                maxMinForYAndX = new int[] { 160, 230, 240, 300 };//
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 127, 271, 176, 322 };//
            }
            else if (indexPhoto == "28")
            {
                maxMinForYAndX = new int[] { 85, 240, 160, 300 };//
            }
            else if (indexPhoto == "29")
            {
                maxMinForYAndX = new int[] { 177, 1, 243, 82 };//
            }
            else if (indexPhoto == "30")
            {
                maxMinForYAndX = new int[] { 143, 1, 177, 82 };//
            }
            else if (indexPhoto == "31")
            {
                maxMinForYAndX = new int[] { 79, 1, 145, 82 };//
            }
            else if (indexPhoto == "32")
            {
                maxMinForYAndX = new int[] { 160, 25, 85, 85 };//
            }
            else if (indexPhoto == "33")
            {
                maxMinForYAndX = new int[] { 176, 0, 127, 51 };//
            }
            else if (indexPhoto == "34")
            {
                maxMinForYAndX = new int[] { 240, 20, 160, 90 };//
            }
            else if (indexPhoto == "35")
            {
                maxMinForYAndX = new int[] { 240, 25, 307, 110 };//
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
                    if (photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10
                       || photoInspection.IndexPhoto == 19 || photoInspection.IndexPhoto == 20 || photoInspection.IndexPhoto == 21 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 23 || photoInspection.IndexPhoto == 24
                       || photoInspection.IndexPhoto == 25 || photoInspection.IndexPhoto == 26 || photoInspection.IndexPhoto == 27 || photoInspection.IndexPhoto == 28 || photoInspection.IndexPhoto == 32 || photoInspection.IndexPhoto == 33
                       || photoInspection.IndexPhoto == 34 || photoInspection.IndexPhoto == 35 )
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14 || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16
                        || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 29 || photoInspection.IndexPhoto == 30 || photoInspection.IndexPhoto == 31)
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