using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMobaileServise.BackgraundService.OrderWork
{
    public class OrderGOToArchive : IJob
    {
        SqlCommandApiMobile sqlCommandApiMobile = null;

        void IJob.Execute()
        {
            sqlCommandApiMobile = new SqlCommandApiMobile();
            Task.Run(() => Work());
        }

        private void Work()
        {
            List<Shipping> shippings = sqlCommandApiMobile.GetShipingPayd();
            foreach(Shipping shipping in shippings)
            {
                if(shipping.DataFullArcive != null && DateTime.Parse(shipping.DataFullArcive) < DateTime.Now)
                {
                    sqlCommandApiMobile.ReCurentStatus(shipping.Id, "Archived");
                }
            }
        }
    }
}