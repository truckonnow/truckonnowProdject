﻿using MDispatch.Models;
using Plugin.Connectivity;
using System.Collections.Generic;

namespace MDispatch.Service
{
    public class ManagerDispatchMob
    {
        public delegate void InitDasbordDelegate();
        private A_R a_R = null;
        private OrderGet orderGet = null;
        private Photo photo = null;
        private Inspection inspection = null;

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

        public int OrderWork(string typeOrder, string token, ref string description, ref List<Shipping> shippings)
        {
            orderGet = new OrderGet();
            int stateOrder = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeOrder == "OrderGet")
                {
                    stateOrder = orderGet.ActiveOreder(token, ref description, ref shippings);
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

        public int Recurent(string token, string id, string status, ref string description)
        {
            inspection = new Inspection();
            int stateInspection = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                stateInspection = inspection.ReCurentStatus(token, id, ref description, status);
            }
            inspection = null;
            return stateInspection;
        }

        public int AskWork(string typeInspection, string token, string id, object obj, ref string description)
        {
            inspection = new Inspection();
            int stateInspection = 1;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (typeInspection == "SaveAsk")
                {
                    stateInspection = inspection.SaveAsk(token, id, (Models.Ask)obj, ref description);
                }
                else if(typeInspection == "SavePhoto")
                {
                    stateInspection = inspection.SavePhoto(token, id, (Models.PhotoInspection)obj, ref description);
                }
                else if (typeInspection == "SaveAsk1")
                {
                    stateInspection = inspection.SaveAsk(token, id, (Models.Ask1)obj, ref description);
                }
                else if (typeInspection == "AskFromUser")
                {
                    stateInspection = inspection.SaveAsk(token, id, (AskFromUser)obj, ref description);
                }
                else if(typeInspection == "FeedBack")
                {
                    stateInspection = inspection.SaveAsk(token, (Models.Feedback)obj, ref description);
                }
            }
            inspection = null;
            return stateInspection;
        }
    }
}