using DaoModels.DAO.Models;
using System.Collections.Generic;

namespace ApiMobaileServise.Servise.AddDamage
{
    public interface ITypeScan
    {
        int GetCordinatY(string indexPhoto, double Y);
        int GetCordinatX(string indexPhoto, double X);
        int[] GetMaxMinForYAndX(string indexPhoto);
        void SetDamage(PhotoInspection photoInspection, string typrCar, string pathScan);
        void SetDamage(List<DamageForUser> damageForUsers, string typrCar, string pathScan);
    }
}