using MDispatch.Models;
using MDispatch.Service;
using Plugin.Settings;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MDispatch.ViewModels.TAbbMV
{
    public class ActiveMV : BindableBase
    {
        private ManagerDispatchMob managerDispatchMob = null;

        public ActiveMV(ManagerDispatchMob managerDispatchMob)
        {
            Shippings = new List<Shipping>();
            //VehiclwInformation vehiclwInformation = new VehiclwInformation();
            //vehiclwInformation.Year = "1992";
            //vehiclwInformation.Make = "Porsche";
            //vehiclwInformation.Model = "911";
            //Shipping shipping = new Shipping();
            //shipping.Id = "2222";
            //shipping.VehiclwInformations = new List<VehiclwInformation>();
            //shipping.VehiclwInformations.Add(vehiclwInformation);
            //shipping.CityP = "dallas";
            //shipping.StateP = "TX";
            //shipping.PickupExactly = "10.01.18";
            //shipping.CityP = "north";
            //shipping.StateP = "OH";
            //shipping.PickupExactly = "11.01.18";
            //Shippings.Add(shipping);
            //Shippings.Add(shipping);
            this.managerDispatchMob = managerDispatchMob;
            Init();
        }

        private List<Shipping> shippings = null;
        public List<Shipping> Shippings
        {
            get => shippings;
            set => SetProperty(ref shippings, value);
        }

        public async void Init()
        {
            string token = CrossSettings.Current.GetValueOrDefault("Token", "");
            string description = null;
            int state = 0;
            List<Shipping> shippings = null;
            await Task.Run(() =>
            {
                state = managerDispatchMob.OrderWork("OrderGet", token, "Assigned", ref description, ref shippings);
            });
            if (state == 1)
            {
                //FeedBack = "Not Network";
            }
            else if (state == 2)
            {
                //FeedBack = description;
            }
            else if (state == 3)
            {
                Shippings = shippings;
            }
            else if (state == 4)
            {
                //FeedBack = "Technical work on the service";
            }
        }
    }
}
