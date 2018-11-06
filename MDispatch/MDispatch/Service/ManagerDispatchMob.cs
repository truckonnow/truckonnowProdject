using MDispatch.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDispatch.Service
{
    public class ManagerDispatchMob
    {
        private A_R a_R = null;
        private OrderGet orderGet = null;

        public int A_RWork(string typeR_A, string login, string password, ref string description, ref string token)
        {
            a_R = new A_R();
            int stateA_R = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeR_A == "authorisation")
                {
                    stateA_R = a_R.Avthorization(login, password, ref description, ref token);
                }
            }
            a_R = null;
            return stateA_R;
        }

        public int OrderWork(string typeOrder, string token, string status, ref string description, ref List<Shipping> shippings)
        {
            orderGet = new OrderGet();
            int stateOrder = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeOrder == "OrderGet")
                {
                    stateOrder = orderGet.ActiveOreder(token, status, ref description, ref shippings);
                }
            }
            orderGet = null;
            return stateOrder;
        }
    }
}