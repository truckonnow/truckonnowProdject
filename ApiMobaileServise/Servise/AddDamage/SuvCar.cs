using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            if (indexPhoto == "5")
            {
                maxMinForYAndX = new int[] { 245, 23, 328, 160 };
            }
            else if (indexPhoto == "6")
            {
                maxMinForYAndX = new int[] { 310, 93, 325, 115 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 310, 93, 325, 115 };
            }
            else if (indexPhoto == "8")
            {
                maxMinForYAndX = new int[] { 328, 23, 382, 105 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 350, 1, 412, 64 };
            }
            else if (indexPhoto == "10")
            {
                maxMinForYAndX = new int[] { 382, 23, 432, 102 };
            }
            else if (indexPhoto == "11")
            {
                maxMinForYAndX = new int[] { 164, 445, 182, 460 };
            }
            else if (indexPhoto == "12")
            {
                maxMinForYAndX = new int[] { 148, 435, 187, 498 };
            }
            else if (indexPhoto == "13")
            {
                maxMinForYAndX = new int[] { 187, 435, 278, 498 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 278, 435, 315, 498 };
            }
            else if (indexPhoto == "15")
            {
                maxMinForYAndX = new int[] { 281, 445, 299, 460 };
            }
            else if (indexPhoto == "16")
            {
                maxMinForYAndX = new int[] { 148, 350, 315, 435 };
            }
            else if (indexPhoto == "17")
            {
                maxMinForYAndX = new int[] { 390, 150, 520, 1 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 520, 150, 615, 1 };
            }
            else if (indexPhoto == "19")
            {
                maxMinForYAndX = new int[] { 130, 605, 350, 860 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 340, 605, 480, 860 };
            }
            else if (indexPhoto == "21")
            {
                maxMinForYAndX = new int[] { 480, 605, 640, 860 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 640, 690, 780, 860 };
            }
            else if (indexPhoto == "23")
            {
                maxMinForYAndX = new int[] { 780, 690, 855, 860 };
            }
            else if (indexPhoto == "24`")
            {
                maxMinForYAndX = new int[] { 865, 620, 995, 530 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 530, 865, 375, 995 };
            }
            else if (indexPhoto == "9")
            {
                maxMinForYAndX = new int[] { 865, 375, 995, 285 };
            }
            else if (indexPhoto == "7")
            {
                maxMinForYAndX = new int[] { 620, 595, 285, 995 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 640, 175, 610, 220 };
            }
            else if (indexPhoto == "25")
            {
                maxMinForYAndX = new int[] { 640, 685, 610, 730 };
            }
            else if (indexPhoto == "18")
            {
                maxMinForYAndX = new int[] { 640, 175, 610, 220 };
            }
            else if (indexPhoto == "27")
            {
                maxMinForYAndX = new int[] { 640, 685, 610, 730 };
            }
            else if (indexPhoto == "24")
            {
                maxMinForYAndX = new int[] { 295, 150, 455, 380 };
            }
            else if (indexPhoto == "22")
            {
                maxMinForYAndX = new int[] { 685, 115, 815, 1 };
            }
            else if (indexPhoto == "32")
            {
                maxMinForYAndX = new int[] { 225, 115, 355, 1 };
            }
            else if (indexPhoto == "20")
            {
                maxMinForYAndX = new int[] { 685, 795, 815, 910 };
            }
            else if (indexPhoto == "14")
            {
                maxMinForYAndX = new int[] { 225, 795, 355, 910 };
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
                    if (photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9
                        || photoInspection.IndexPhoto == 10)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 11)
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