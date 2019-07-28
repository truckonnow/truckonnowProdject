using DaoModels.DAO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMobaileServise.Servise.AddDamage
{
    public interface ITypeScan
    {
        int GetCordinatY(string indexPhoto, double Y);
        int GetCordinatX(string indexPhoto, double X);
        int[] GetMaxMinForYAndX(string indexPhoto);
        Task SetDamage(PhotoInspection photoInspection, string typrCar, string pathScan);
        Task SetDamage(List<DamageForUser> damageForUsers, string typrCar, string pathScan);
    }
}