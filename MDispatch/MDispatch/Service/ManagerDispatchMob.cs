using MDispatch.Models;
using Plugin.Connectivity;
using System.Collections.Generic;

namespace MDispatch.Service
{
    public class ManagerDispatchMob
    {
        private A_R a_R = null;
        private OrderGet orderGet = null;
        private Photo photo = null;

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


        public int OrderOneWork(string typeOrder, string id, string token, string idOrder, string name, string contactName, string address,
            string city, string state, string zip, string phone, string email, string typeSave, ref string description)
        {
            orderGet = new OrderGet();
            int stateOrder = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeOrder == "Save")
                {
                    stateOrder = orderGet.Save(token, id, idOrder, name, contactName, address, city, state, zip, phone, email, typeSave, ref description);
                }
            }
            orderGet = null;
            return stateOrder;
        }

        public int OrderOneWork(string typeOrder, string id, string token, string typeSave, string payment, string paymentTeams, ref string description)
        {
            orderGet = new OrderGet();
            int stateOrder = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeOrder == "Save")
                {
                    stateOrder = orderGet.Save(token, id, typeSave, payment, paymentTeams, ref description);
                }
            }
            orderGet = null;
            return stateOrder;
        }

        public int PhotoWork(string typePhoto, string token, string id, byte[] PhotoInArrayByte, ref string description)
        {
            photo = new Photo();
            int statePhoto = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typePhoto == "SavePhoto")
                {
                    statePhoto = photo.SaveTakeNewPhoto(token, id, PhotoInArrayByte, ref description);
                }
            }
            photo = null;
            return statePhoto;
        }
    }
}