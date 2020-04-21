using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise.AddDamage
{
    public class TricycleMotorcycle : ITypeScan
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
            switch (indexPhoto)
            {
                case "4": maxMinForYAndX = new int[] { 695, 233, 430, 55 }; break;
                case "5": maxMinForYAndX = new int[] { 622, 233, 578, 174 }; break;
                case "6": maxMinForYAndX = new int[] { 880, 220, 635, 25 }; break;
                case "7": maxMinForYAndX = new int[] { 853, 178, 700, 25 }; break;
                case "8": maxMinForYAndX = new int[] { 372, 815, 235, 995 }; break;
                case "9": maxMinForYAndX = new int[] { 480, 875, 315, 995 }; break;
                case "10": maxMinForYAndX = new int[] { 495, 775, 300, 895 }; break;
                case "11": maxMinForYAndX = new int[] { 560, 815, 423, 995 }; break;
                case "12": maxMinForYAndX = new int[] { 560, 945, 235, 995 }; break;
                case "13": maxMinForYAndX = new int[] { 635, 582, 880, 777 }; break;
                case "14": maxMinForYAndX = new int[] { 700, 619, 853, 777 }; break;
                case "15": maxMinForYAndX = new int[] { 578, 564, 622, 625 }; break;
                case "16": maxMinForYAndX = new int[] { 430, 569, 695, 747 }; break;
                case "17": maxMinForYAndX = new int[] { 220, 560, 490, 740 }; break;
                case "18": maxMinForYAndX = new int[] { 190, 615, 340, 765 }; break;
                case "19": maxMinForYAndX = new int[] { 430, 155, 515, 90 }; break;
                case "20": maxMinForYAndX = new int[] { 252, 255, 555, 100 }; break;
                case "22": maxMinForYAndX = new int[] { 340, 185, 190, 35 }; break;
                case "23": maxMinForYAndX = new int[] { 490, 242, 220, 62 }; break;
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
                    if (photoInspection.IndexPhoto == 4 || photoInspection.IndexPhoto == 5 || photoInspection.IndexPhoto == 6 || photoInspection.IndexPhoto == 7 || photoInspection.IndexPhoto == 13 || photoInspection.IndexPhoto == 14
                        || photoInspection.IndexPhoto == 15 || photoInspection.IndexPhoto == 16 || photoInspection.IndexPhoto == 17 || photoInspection.IndexPhoto == 18 || photoInspection.IndexPhoto == 22 || photoInspection.IndexPhoto == 23)
                    {
                        g.DrawImage(img2, x, y);
                    }
                    else if (photoInspection.IndexPhoto == 8 || photoInspection.IndexPhoto == 9 || photoInspection.IndexPhoto == 10 || photoInspection.IndexPhoto == 11 || photoInspection.IndexPhoto == 12 || photoInspection.IndexPhoto == 19
                        || photoInspection.IndexPhoto == 20)
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