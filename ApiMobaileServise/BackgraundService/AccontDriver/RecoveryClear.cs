using ApiMobaileServise.Servise;
using DaoModels.DAO.Models;
using FluentScheduler;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ApiMobaileServise.BackgraundService.AccontDriver
{
    public class RecoveryClear : IJob
    {
        SqlCommandApiMobile sqlCommandApiMobile = null;

        public void Execute()
        {
            sqlCommandApiMobile = new SqlCommandApiMobile();
            CheackRecavery();
        }

        private void CheackRecavery()
        {
            try
            {
                List<PasswordRecovery> passwordRecoveries = sqlCommandApiMobile.GetPasswordRecovery().Where(p => Convert.ToDateTime(p.Date) > DateTime.Now).ToList();
                sqlCommandApiMobile.RemovePasswordRecoveriesRange(passwordRecoveries);
            }
            catch
            { }
        }
    }
}