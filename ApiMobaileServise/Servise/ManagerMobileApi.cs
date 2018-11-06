using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;

namespace ApiMobaileServise.Servise
{
    public class ManagerMobileApi
    {
        SqlCommandApiMobile sqlCommandApiMobile = null;
          
        public ManagerMobileApi()
        {
            sqlCommandApiMobile = new SqlCommandApiMobile();
        }

        public string Avtorization(string email, string password)
        {
            string token = "";
            if (sqlCommandApiMobile.CheckEmailAndPsw(email, password))
            {
                token = CreateToken(email, password);
                sqlCommandApiMobile.SaveToken(email, password, token);
            }
            return token;
        }

        public bool GetOrdersForToken(string token, string status, ref List<Shipping> shippings)
        {
            bool isToken = sqlCommandApiMobile.CheckToken(token);
            if (isToken)
            {
                shippings = sqlCommandApiMobile.GetOrdersForToken(token, status);
            }
            return isToken;
        }

        private string CreateToken(string email, string password)
        {
            string token = "";
            for(int i = 0; i < email.Length; i++)
            {
                token += i * new Random().Next(1, 1000) + email[i]; 
            }
            for (int i = 0; i < password.Length; i++)
            {
                token += i * new Random().Next(1, 1000) + password[i];
            }
            return token;
        }
    }
}