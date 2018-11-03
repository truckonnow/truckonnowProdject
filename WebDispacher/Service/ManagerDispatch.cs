using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDispacher.Dao;

namespace WebDispacher.Service
{
    public class ManagerDispatch
    {
        public SqlCommadWebDispatch _sqlEntityFramworke = null;

        public ManagerDispatch()
        {
            _sqlEntityFramworke = new SqlCommadWebDispatch();
        }

        public bool Avthorization(string login, string password)
        {
            return _sqlEntityFramworke.ExistsDataUser(login, password);
        }

        public bool CheckKey(string key)
        {
            return _sqlEntityFramworke.CheckKeyDb(key);
        }

        public List<Driver> GetDrivers(int pag)
        {
            return _sqlEntityFramworke.GetDrivers( pag);
        }

        public void Assign(string idOrder, string idDriver)
        {
            _sqlEntityFramworke.AddDriversInOrder(idOrder, idDriver);
        }

        public void Unassign(string idOrder)
        {
            _sqlEntityFramworke.RemoveDriversInOrder(idOrder);
        }

        //public Shipping GetDriver()
        //{
        //    return _sqlEntityFramworke.GetShipping(id);
        //}

        public int Createkey(string login, string password)
        {
            Random random = new Random();
            int key = random.Next(1000, 1000000000);
            _sqlEntityFramworke.SaveKeyDatabays(login, password, key);
            return key;
        }

        public List<Shipping> GetOrders(string status, int page)
        {
            return _sqlEntityFramworke.GetShippings(status, page);
        }

        public Shipping GetOrder(string id)
        {
            return _sqlEntityFramworke.GetShipping(id);
        }
    }
}
